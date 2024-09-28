terraform {
  required_providers {
    spot = {
      source = "rackerlabs/spot"
    }
    kubernetes = {
      source  = "hashicorp/kubernetes"
      version = ">= 2.7.0"
    }
  }
}

provider "spot" {
  token = var.token
}

data "spot_kubeconfig" "example" {
  cloudspace_name = var.cloudspace_name
}

provider "kubernetes" {
  host     = data.spot_kubeconfig.example.kubeconfigs[0].host
  token    = data.spot_kubeconfig.example.kubeconfigs[0].token
  insecure = data.spot_kubeconfig.example.kubeconfigs[0].insecure
}

# Comprobar si el deployment existe usando kubectl
resource "null_resource" "check_deployment_exists" {
  provisioner "local-exec" {
    command = "kubectl get deployment colombia-web-deployment --namespace=default || echo 'not found'"
  }

  triggers = {
    always_run = timestamp()
  }
}

# Crear el deployment solo si no existe
resource "kubernetes_deployment" "colombia_web" {
  count = "${(self.triggers.always_run == "not found") ? 1 : 0}"

  metadata {
    name = "colombia-web-deployment"
    labels = {
      app = "colombia-web"
    }
  }

  spec {
    replicas = 1

    selector {
      match_labels = {
        app = "colombia-web"
      }
    }

    template {
      metadata {
        labels = {
          app = "colombia-web"
        }
        annotations = {
          "deployment.kubernetes.io/restartedAt" = timestamp()
        }
      }

      spec {
        container {
          image = "ghcr.io/carandev/colombiaweb:main"
          name  = "colombia-web-container"

          port {
            container_port = 8080
          }

          env {
            name  = "ASPNETCORE_ENVIRONMENT"
            value = "Production"
          }

          env {
            name  = "COLOMBIA_API"
            value = "https://api-colombia.com"
          }
        }

        image_pull_secrets {
          name = "github-pkg-secret"
        }
      }
    }
  }

  lifecycle {
    ignore_changes = [
      metadata[0].annotations["deployment.kubernetes.io/restartedAt"]
    ]
  }
}

# Comprobar si el servicio existe usando kubectl
resource "null_resource" "check_service_exists" {
  provisioner "local-exec" {
    command = "kubectl get service colombia-web-service --namespace=default || echo 'not found'"
  }

  triggers = {
    always_run = timestamp()
  }
}

# Crear el servicio solo si no existe
resource "kubernetes_service" "colombia_web_service" {
  count = "${(self.triggers.always_run == 'not found') ? 1 : 0}"

  metadata {
    name = "colombia-web-service"
  }

  spec {
    selector = {
      app = "colombia-web"
    }

    port {
      port        = 80
      target_port = 8080
      node_port   = 30003
    }

    type = "NodePort"
  }
}
