#r "../_lib/Fornax.Core.dll"

type SiteInfo = {
    title: string
    description: string
    theme_variant: string option
    numbers_in_menu: bool
    root_url: string
}

let config = {
    title = "FancyApp"
    description = "Description of FancyApp project"
    theme_variant = Some "blue"
    numbers_in_menu = true
    root_url = "http://kcieslak.io/FancyApp"
}

let loader (projectRoot: string) (siteContet: SiteContents) =
    siteContet.Add(config)

    siteContet