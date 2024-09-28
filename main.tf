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

# Obtener los detalles del kubeconfig de Rackspace
data "spot_kubeconfig" "example" {
  cloudspace_name = var.cloudspace_name
}

# Proveedor de Kubernetes usando la configuración obtenida de spot_kubeconfig
provider "kubernetes" {
  host     = data.spot_kubeconfig.example.kubeconfigs[0].host
  token    = data.spot_kubeconfig.example.kubeconfigs[0].token
  insecure = data.spot_kubeconfig.example.kubeconfigs[0].insecure
}

# Recurso Deployment de Kubernetes
resource "kubernetes_deployment" "colombia_web" {
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
}

# Recurso Service de Kubernetes
resource "kubernetes_service" "colombia_web_service" {
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
    }

    type = "NodePort"

    node_port = 30003
  }
}
