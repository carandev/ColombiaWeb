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

# Verificar si ya existe el deployment
data "kubernetes_deployment" "existing_colombia_web" {
  metadata {
    name      = "colombia-web-deployment"
    namespace = "default"
  }
}

# Verificar si ya existe el servicio
data "kubernetes_service" "existing_colombia_web_service" {
  metadata {
    name      = "colombia-web-service"
    namespace = "default"
  }
}

# Recurso Deployment de Kubernetes
resource "kubernetes_deployment" "colombia_web" {
  count = length(data.kubernetes_deployment.existing_colombia_web.metadata.0.name) == 0 ? 1 : 0

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

# Recurso para reiniciar el Deployment si ya existe
resource "null_resource" "restart_colombia_web" {
  count = length(data.kubernetes_deployment.existing_colombia_web.metadata.0.name) > 0 ? 1 : 0

  triggers = {
    timestamp = timestamp()
  }

  provisioner "local-exec" {
    command = "kubectl rollout restart deployment colombia-web-deployment"
  }
}

# Recurso Service de Kubernetes, solo si no existe
resource "kubernetes_service" "colombia_web_service" {
  count = length(data.kubernetes_service.existing_colombia_web_service.metadata.0.name) == 0 ? 1 : 0

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
