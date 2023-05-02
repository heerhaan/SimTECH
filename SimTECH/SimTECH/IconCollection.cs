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
        // Q
        // R
        // S
        public static string Skull => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-skull"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M12 4c4.418 0 8 3.358 8 7.5c0 1.901 -.755 3.637 -2 4.96l0 2.54a1 1 0 0 1 -1 1h-10a1 1 0 0 1 -1 -1v-2.54c-1.245 -1.322 -2 -3.058 -2 -4.96c0 -4.142 3.582 -7.5 8 -7.5z""></path>
           <path d=""M10 17v3""></path>
           <path d=""M14 17v3""></path>
           <path d=""M9 11m-1 0a1 1 0 1 0 2 0a1 1 0 1 0 -2 0""></path>
           <path d=""M15 11m-1 0a1 1 0 1 0 2 0a1 1 0 1 0 -2 0""></path>
        </svg>";
        public static string SteeringWheel => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-steering-wheel"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M12 12m-9 0a9 9 0 1 0 18 0a9 9 0 1 0 -18 0""></path>
           <path d=""M12 12m-2 0a2 2 0 1 0 4 0a2 2 0 1 0 -4 0""></path>
           <path d=""M12 14l0 7""></path>
           <path d=""M10 12l-6.75 -2""></path>
           <path d=""M14 12l6.75 -2""></path>
        </svg>";
        // T
        public static string TrafficCone => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-traffic-cone"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M4 20l16 0""></path>
           <path d=""M9.4 10l5.2 0""></path>
           <path d=""M7.8 15l8.4 0""></path>
           <path d=""M6 20l5 -15h2l5 15""></path>
        </svg>";
        // U
        // V
        // W
        // X
        // Y
        // Z
    }
}
