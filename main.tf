provider "spot" {
  token = var.token  # Usa un secret para el token
}

data "spot_kubeconfig" "example" {
  cloudspace_name = var.cloudspace_name
}

output "kubeconfig" {
  value     = data.spot_kubeconfig.example
  sensitive = true  # Esto oculta el valor en la salida
}
