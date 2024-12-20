resource "cloudflare_record" "price-alert" {
  zone_id = var.CLOUDFLARE_ZONE_ID
  name    = "price-alert"
  type    = "A"
  content = digitalocean_droplet.price-alert-droplet.ipv4_address
  proxied = true
  ttl     = 1
}
