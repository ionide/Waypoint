#r "../../_lib/Fornax.Core.dll"
#if !FORNAX
#load "../../loaders/contentloader.fsx"
#load "../../loaders/pageloader.fsx"
#load "../../loaders/globalloader.fsx"
#endif

open Html

let header (ctx : SiteContents) page =
    let siteInfo = ctx.TryGetValue<Globalloader.SiteInfo>().Value
    let rootUrl = siteInfo.root_url

    head [] [
        meta [CharSet "utf-8"]
        meta [Name "viewport"; Content "width=device-width, initial-scale=1"]
        title [] [!! (siteInfo.title + " | " + page)]
        link [Rel "icon"; Type "image/png"; Sizes "32x32"; Href (rootUrl.subRoute "/static/images/favicon.png")]
        link [Rel "stylesheet"; Href (rootUrl.subRoute "/static/css/atom-one-dark-reasonable.css")]
        link [Rel "stylesheet"; Href (rootUrl.subRoute "/static/css/theme.css")]
        link [Rel "stylesheet"; Href (rootUrl.subRoute "/static/css/tips.css")]

        link [Rel "stylesheet"; Href "//cdnjs.cloudflare.com/ajax/libs/JavaScript-autoComplete/1.0.4/auto-complete.min.css"]
        link [Rel "stylesheet"; Href "//cdnjs.cloudflare.com/ajax/libs/jquery.perfect-scrollbar/1.5.0/css/perfect-scrollbar.min.css"]
        link [Rel "stylesheet"; Href "//cdnjs.cloudflare.com/ajax/libs/font-awesome/5.12.0-2/css/all.min.css"]
        link [Rel "stylesheet"; Href "//cdnjs.cloudflare.com/ajax/libs/highlight.js/10.0.0/styles/atom-one-dark.min.css"]
        link [Rel "stylesheet"; Href "//cdnjs.cloudflare.com/ajax/libs/featherlight/1.7.13/featherlight.min.css"]

        if siteInfo.theme_variant.IsSome then
            link [Rel "stylesheet"; Href (rootUrl.subRoutef "/static/css/theme-%s.css" siteInfo.theme_variant.Value)]

        script [Src "//cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js"] []
    ]
