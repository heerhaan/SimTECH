namespace SimTECH
{
    // Small = 24px
    public static class IconCollection
    {
        public static string[] ArrayOfIcons => new[]
        {
            BrandCupra, BrandMercedes, BrandToyota, BrandVolkswagen,
            CarCrash, CrossFilled, Engine, Helmet, Skull, SteeringWheel, TrafficCone
        };

        // A
        public static string ArrowDown => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-arrow-badge-down-filled"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M16.375 6.22l-4.375 3.498l-4.375 -3.5a1 1 0 0 0 -1.625 .782v6a1 1 0 0 0 .375 .78l5 4a1 1 0 0 0 1.25 0l5 -4a1 1 0 0 0 .375 -.78v-6a1 1 0 0 0 -1.625 -.78z"" stroke-width=""0"" fill=""currentColor""></path>
        </svg>";
        public static string ArrowUp => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-arrow-badge-up-filled"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M11.375 6.22l-5 4a1 1 0 0 0 -.375 .78v6l.006 .112a1 1 0 0 0 1.619 .669l4.375 -3.501l4.375 3.5a1 1 0 0 0 1.625 -.78v-6a1 1 0 0 0 -.375 -.78l-5 -4a1 1 0 0 0 -1.25 0z"" stroke-width=""0"" fill=""currentColor""></path>
        </svg>";
        // B
        public static string BrandCupra => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-brand-cupra"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M4.5 10l-2.5 -4l15.298 6.909a.2 .2 0 0 1 .09 .283l-3.388 5.808""></path>
           <path d=""M10 19l-3.388 -5.808a.2 .2 0 0 1 .09 -.283l15.298 -6.909l-2.5 4""></path>
        </svg>";
        public static string BrandMercedes => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-brand-mercedes"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M12 12m-9 0a9 9 0 1 0 18 0a9 9 0 1 0 -18 0""></path>
           <path d=""M12 3v9""></path>
           <path d=""M12 12l7 5""></path>
           <path d=""M12 12l-7 5""></path>
        </svg>";
        public static string BrandToyota => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-brand-toyota"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M12 12m-10 0a10 7 0 1 0 20 0a10 7 0 1 0 -20 0""></path>
           <path d=""M9 12c0 3.866 1.343 7 3 7s3 -3.134 3 -7s-1.343 -7 -3 -7s-3 3.134 -3 7z""></path>
           <path d=""M6.415 6.191c-.888 .503 -1.415 1.13 -1.415 1.809c0 1.657 3.134 3 7 3s7 -1.343 7 -3c0 -.678 -.525 -1.304 -1.41 -1.806""></path>
        </svg>";
        public static string BrandVolkswagen => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-brand-volkswagen"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M12 21a9 9 0 0 0 9 -9a9 9 0 0 0 -9 -9a9 9 0 0 0 -9 9a9 9 0 0 0 9 9z""></path>
           <path d=""M5 7l4.5 11l1.5 -5h2l1.5 5l4.5 -11""></path>
           <path d=""M9 4l2 6h2l2 -6""></path>
        </svg>";
        // C
        public static string Car => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-car"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M7 17m-2 0a2 2 0 1 0 4 0a2 2 0 1 0 -4 0""></path>
           <path d=""M17 17m-2 0a2 2 0 1 0 4 0a2 2 0 1 0 -4 0""></path>
           <path d=""M5 17h-2v-6l2 -5h9l4 5h1a2 2 0 0 1 2 2v4h-2m-4 0h-6m-6 -6h15m-6 0v-5""></path>
        </svg>";
        public static string CarCrash => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-car-crash"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M10 17m-2 0a2 2 0 1 0 4 0a2 2 0 1 0 -4 0""></path>
           <path d=""M7 6l4 5h1a2 2 0 0 1 2 2v4h-2m-4 0h-5m0 -6h8m-6 0v-5m2 0h-4""></path>
           <path d=""M14 8v-2""></path>
           <path d=""M19 12h2""></path>
           <path d=""M17.5 15.5l1.5 1.5""></path>
           <path d=""M17.5 8.5l1.5 -1.5""></path>
        </svg>";
        public static string CloudBolt => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-cloud-storm"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M7 18a4.6 4.4 0 0 1 0 -9a5 4.5 0 0 1 11 2h1a3.5 3.5 0 0 1 0 7h-1""></path>
           <path d=""M13 14l-2 4l3 0l-2 4""></path>
        </svg>";
        public static string CrossFilled => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-cross-filled"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M10 2l-.117 .007a1 1 0 0 0 -.883 .993v4h-4a1 1 0 0 0 -1 1v4l.007 .117a1 1 0 0 0 .993 .883h4v8a1 1 0 0 0 1 1h4l.117 -.007a1 1 0 0 0 .883 -.993v-8h4a1 1 0 0 0 1 -1v-4l-.007 -.117a1 1 0 0 0 -.993 -.883h-4v-4a1 1 0 0 0 -1 -1h-4z"" stroke-width=""0"" fill=""currentColor""></path>
        </svg>";
        // D
        // E
        public static string Engine => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-engine"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M3 10v6""></path>
           <path d=""M12 5v3""></path>
           <path d=""M10 5h4""></path>
           <path d=""M5 13h-2""></path>
           <path d=""M6 10h2l2 -2h3.382a1 1 0 0 1 .894 .553l1.448 2.894a1 1 0 0 0 .894 .553h1.382v-2h2a1 1 0 0 1 1 1v6a1 1 0 0 1 -1 1h-2v-2h-3v2a1 1 0 0 1 -1 1h-3.465a1 1 0 0 1 -.832 -.445l-1.703 -2.555h-2v-6z""></path>
        </svg>";
        public static string EngineOff => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-engine-off"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M3 10v6""></path>
           <path d=""M12 5v3""></path>
           <path d=""M10 5h4""></path>
           <path d=""M5 13h-2""></path>
           <path d=""M16 16h-1v2a1 1 0 0 1 -1 1h-3.465a1 1 0 0 1 -.832 -.445l-1.703 -2.555h-2v-6h2l.99 -.99m3.01 -1.01h1.382a1 1 0 0 1 .894 .553l1.448 2.894a1 1 0 0 0 .894 .553h1.382v-2h2a1 1 0 0 1 1 1v6""></path>
           <path d=""M3 3l18 18""></path>
        </svg>";
        public static string Equal => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-equal"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M5 10h14""></path>
           <path d=""M5 14h14""></path>
        </svg>";
        // F
        // G
        // H
        public static string Helmet => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-helmet"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M12 4a9 9 0 0 1 5.656 16h-11.312a9 9 0 0 1 5.656 -16z""></path>
           <path d=""M20 9h-8.8a1 1 0 0 0 -.968 1.246c.507 2 1.596 3.418 3.268 4.254c2 1 4.333 1.5 7 1.5""></path>
        </svg>";
        public static string HelmetOff => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-helmet-off"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M8.633 4.654a9 9 0 0 1 11.718 11.7m-1.503 2.486a9.008 9.008 0 0 1 -1.192 1.16h-11.312a9 9 0 0 1 -.185 -13.847""></path>
           <path d=""M20 9h-7m-2.768 1.246c.507 2 1.596 3.418 3.268 4.254c.524 .262 1.07 .49 1.64 .683""></path>
           <path d=""M3 3l18 18""></path>
        </svg>";
        // I
        // J
        // K
        // L
        // M
        // N
        // O
        // P
        public static string Polygon => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-polygon"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""inherit"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M12 5m-2 0a2 2 0 1 0 4 0a2 2 0 1 0 -4 0""></path>
           <path d=""M19 8m-2 0a2 2 0 1 0 4 0a2 2 0 1 0 -4 0""></path>
           <path d=""M5 11m-2 0a2 2 0 1 0 4 0a2 2 0 1 0 -4 0""></path>
           <path d=""M15 19m-2 0a2 2 0 1 0 4 0a2 2 0 1 0 -4 0""></path>
           <path d=""M6.5 9.5l3.5 -3""></path>
           <path d=""M14 5.5l3 1.5""></path>
           <path d=""M18.5 10l-2.5 7""></path>
           <path d=""M13.5 17.5l-7 -5""></path>
        </svg>";
        public static string Pyramid => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-pyramid"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M3 17l9 4l9 -4l-9 -14z""></path>
           <path d=""M12 3v18""></path>
        </svg>";
        // Q
        // R
        // S
        public static string Slashes => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-slashes"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M14 5l-10 14""></path>
           <path d=""M20 5l-10 14""></path>
        </svg>";
        public static string Skull => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-skull"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M12 4c4.418 0 8 3.358 8 7.5c0 1.901 -.755 3.637 -2 4.96l0 2.54a1 1 0 0 1 -1 1h-10a1 1 0 0 1 -1 -1v-2.54c-1.245 -1.322 -2 -3.058 -2 -4.96c0 -4.142 3.582 -7.5 8 -7.5z""></path>
           <path d=""M10 17v3""></path>
           <path d=""M14 17v3""></path>
           <path d=""M9 11m-1 0a1 1 0 1 0 2 0a1 1 0 1 0 -2 0""></path>
           <path d=""M15 11m-1 0a1 1 0 1 0 2 0a1 1 0 1 0 -2 0""></path>
        </svg>";
        public static string Snowflake => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-snowflake"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M10 4l2 1l2 -1""></path>
           <path d=""M12 2v6.5l3 1.72""></path>
           <path d=""M17.928 6.268l.134 2.232l1.866 1.232""></path>
           <path d=""M20.66 7l-5.629 3.25l.01 3.458""></path>
           <path d=""M19.928 14.268l-1.866 1.232l-.134 2.232""></path>
           <path d=""M20.66 17l-5.629 -3.25l-2.99 1.738""></path>
           <path d=""M14 20l-2 -1l-2 1""></path>
           <path d=""M12 22v-6.5l-3 -1.72""></path>
           <path d=""M6.072 17.732l-.134 -2.232l-1.866 -1.232""></path>
           <path d=""M3.34 17l5.629 -3.25l-.01 -3.458""></path>
           <path d=""M4.072 9.732l1.866 -1.232l.134 -2.232""></path>
           <path d=""M3.34 7l5.629 3.25l2.99 -1.738""></path>
        </svg>";
        public static string Spade => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-spade-filled"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M11.327 2.26a1395.065 1395.065 0 0 0 -4.923 4.504c-.626 .6 -1.212 1.21 -1.774 1.843a6.528 6.528 0 0 0 -.314 8.245l.14 .177c1.012 1.205 2.561 1.755 4.055 1.574l.246 -.037l-.706 2.118a1 1 0 0 0 .949 1.316h6l.118 -.007a1 1 0 0 0 .83 -1.31l-.688 -2.065l.104 .02c1.589 .25 3.262 -.387 4.32 -1.785a6.527 6.527 0 0 0 -.311 -8.243a31.787 31.787 0 0 0 -1.76 -1.83l-4.938 -4.518a1 1 0 0 0 -1.348 -.001z"" stroke-width=""0"" fill=""currentColor""></path>
        </svg>";
        public static string SteeringWheel => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-steering-wheel"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M12 12m-9 0a9 9 0 1 0 18 0a9 9 0 1 0 -18 0""></path>
           <path d=""M12 12m-2 0a2 2 0 1 0 4 0a2 2 0 1 0 -4 0""></path>
           <path d=""M12 14l0 7""></path>
           <path d=""M10 12l-6.75 -2""></path>
           <path d=""M14 12l6.75 -2""></path>
        </svg>";
        public static string Storm => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-storm"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M12 12m-3 0a3 3 0 1 0 6 0a3 3 0 1 0 -6 0""></path>
           <path d=""M12 12m-7 0a7 7 0 1 0 14 0a7 7 0 1 0 -14 0""></path>
           <path d=""M5.369 14.236c-1.839 -3.929 -1.561 -7.616 -.704 -11.236""></path>
           <path d=""M18.63 9.76c1.837 3.928 1.561 7.615 .703 11.236""></path>
        </svg>";
        // T
        public static string Target => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-playstation-circle"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M12 21a9 9 0 0 0 9 -9a9 9 0 0 0 -9 -9a9 9 0 0 0 -9 9a9 9 0 0 0 9 9z""></path>
           <path d=""M12 12m-4.5 0a4.5 4.5 0 1 0 9 0a4.5 4.5 0 1 0 -9 0""></path>
        </svg>";
        public static string TrafficCone => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-traffic-cone"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M4 20l16 0""></path>
           <path d=""M9.4 10l5.2 0""></path>
           <path d=""M7.8 15l8.4 0""></path>
           <path d=""M6 20l5 -15h2l5 15""></path>
        </svg>";
        public static string TrafficLights => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-traffic-lights"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M7 2m0 5a5 5 0 0 1 5 -5h0a5 5 0 0 1 5 5v10a5 5 0 0 1 -5 5h0a5 5 0 0 1 -5 -5z""></path>
           <path d=""M12 7m-1 0a1 1 0 1 0 2 0a1 1 0 1 0 -2 0""></path>
           <path d=""M12 12m-1 0a1 1 0 1 0 2 0a1 1 0 1 0 -2 0""></path>
           <path d=""M12 17m-1 0a1 1 0 1 0 2 0a1 1 0 1 0 -2 0""></path>
        </svg>";
        public static string Trophy => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-trophy-filled"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M17 3a1 1 0 0 1 .993 .883l.007 .117v2.17a3 3 0 1 1 0 5.659v.171a6.002 6.002 0 0 1 -5 5.917v2.083h3a1 1 0 0 1 .117 1.993l-.117 .007h-8a1 1 0 0 1 -.117 -1.993l.117 -.007h3v-2.083a6.002 6.002 0 0 1 -4.996 -5.692l-.004 -.225v-.171a3 3 0 0 1 -3.996 -2.653l-.003 -.176l.005 -.176a3 3 0 0 1 3.995 -2.654l-.001 -2.17a1 1 0 0 1 1 -1h10zm-12 5a1 1 0 1 0 0 2a1 1 0 0 0 0 -2zm14 0a1 1 0 1 0 0 2a1 1 0 0 0 0 -2z"" stroke-width=""0"" fill=""currentColor""></path>
        </svg>";
        // U
        // V
        // W
        // X
        public static string XCircle => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-playstation-x"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""inherit"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M12 21a9 9 0 0 0 9 -9a9 9 0 0 0 -9 -9a9 9 0 0 0 -9 9a9 9 0 0 0 9 9z""></path>
           <path d=""M8.5 8.5l7 7""></path>
           <path d=""M8.5 15.5l7 -7""></path>
        </svg>";
        // Y
        // Z
    }
}
