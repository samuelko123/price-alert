resource "digitalocean_ssh_key" "price-alert-ssh-key" {
  name       = "price-alert-ssh-key"
  public_key = file("digital_ocean_id_rsa.pub")
}

resource "digitalocean_droplet" "price-alert-droplet" {
  image     = "ubuntu-24-10-x64"
  name      = "price-alert-droplet"
  region    = "syd1"
  size      = "s-1vcpu-512mb-10gb"
  backups   = false
  ssh_keys  = [digitalocean_ssh_key.price-alert-ssh-key.fingerprint]
  user_data = file("digital_ocean_droplet_init.yaml")
}
