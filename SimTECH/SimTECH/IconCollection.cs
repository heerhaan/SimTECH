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

        public static string FrankMadeThis => @"<svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" width=""24"" height=""24"" viewBox=""0, 0, 24,24"" version=""1.1"">
            <path stroke=""none"" fill=""#000000"" fill-rule=""evenodd""></path>
            <path d=""M323.438 1.227 C 289.395 10.136,272.494 50.597,290.406 80.302 C 292.048 83.026,292.844 85.118,292.447 85.670 C 291.976 86.325,232.004 138.577,217.051 151.361 C 215.883 152.359,215.423 152.123,212.754 149.157 C 208.726 144.678,208.598 144.626,144.834 121.418 L 89.277 101.198 92.417 97.474 L 95.557 93.750 104.089 93.750 C 113.549 93.750,117.187 91.932,117.187 87.206 C 117.187 81.848,114.233 79.688,106.903 79.688 C 102.357 79.688,102.182 79.615,102.665 77.930 C 109.351 54.598,92.444 26.688,68.251 21.122 L 64.237 20.199 63.911 15.072 C 63.123 2.693,50.090 2.912,50.025 15.305 L 50.000 20.064 45.809 21.002 C 6.834 29.724,-2.973 86.074,31.017 105.993 L 34.406 107.979 31.569 111.120 C 30.008 112.847,27.628 116.589,26.280 119.435 L 23.828 124.609 23.615 188.057 L 23.402 251.504 25.108 255.133 C 27.064 259.294,31.059 263.470,34.414 264.860 L 36.719 265.815 36.719 322.331 C 36.719 387.874,36.298 384.079,44.469 392.250 C 57.577 405.358,79.535 401.844,87.663 385.336 L 90.234 380.112 90.625 300.353 L 91.016 220.593 125.781 233.481 C 165.024 248.029,167.330 248.575,175.917 245.348 C 191.606 239.453,195.779 218.461,183.654 206.426 L 179.934 202.734 241.655 149.079 L 303.376 95.424 305.796 97.271 C 332.397 117.560,374.422 104.969,386.839 72.990 C 402.553 32.523,365.238 -9.712,323.438 1.227 M367.950 29.273 C 370.758 32.913,371.127 33.785,370.285 34.799 C 368.388 37.084,314.021 83.985,313.007 84.211 C 312.448 84.335,310.302 82.632,308.239 80.426 L 304.487 76.415 306.062 74.840 C 310.325 70.577,363.735 24.459,364.144 24.688 C 364.404 24.833,366.117 26.896,367.950 29.273 M50.000 40.838 L 50.000 47.301 47.186 48.816 C 24.298 61.142,42.819 96.549,66.012 84.807 L 69.789 82.894 75.280 86.101 L 80.771 89.308 78.081 91.616 C 65.160 102.704,45.717 101.856,33.505 89.672 C 16.368 72.574,24.225 42.279,47.656 35.107 C 48.730 34.778,49.697 34.479,49.805 34.442 C 49.912 34.405,50.000 37.283,50.000 40.838 M73.829 38.631 C 84.790 45.054,91.365 59.295,89.134 71.778 L 88.162 77.219 82.753 73.965 C 77.479 70.793,77.344 70.631,77.344 67.529 C 77.344 59.845,73.100 52.235,67.112 49.180 L 64.063 47.624 64.063 41.034 L 64.063 34.444 67.357 35.607 C 69.169 36.246,72.082 37.607,73.829 38.631 M145.140 136.749 C 211.767 161.025,208.424 159.067,198.259 167.847 C 194.705 170.918,191.420 173.432,190.958 173.434 C 190.497 173.436,168.700 165.493,142.521 155.783 C 116.341 146.073,93.955 137.777,92.773 137.347 L 90.625 136.566 90.625 126.877 C 90.625 121.548,90.811 117.188,91.039 117.188 C 91.266 117.188,115.612 125.990,145.140 136.749 M70.807 140.336 C 74.097 141.897,72.845 139.570,92.253 180.218 C 95.384 186.775,94.714 186.445,135.664 201.572 C 175.200 216.176,175.620 216.396,176.349 222.863 C 176.707 226.041,176.489 226.664,174.115 229.235 C 169.375 234.371,170.399 234.600,127.310 218.768 C 86.978 203.950,83.234 202.321,80.418 198.367 C 77.826 194.726,57.716 152.295,57.311 149.609 C 56.261 142.666,64.210 137.205,70.807 140.336 M137.839 169.245 C 159.294 177.182,177.269 183.921,177.782 184.221 C 178.377 184.568,176.186 186.988,171.739 190.896 L 164.762 197.026 135.353 186.195 L 105.944 175.365 101.523 166.393 C 95.943 155.071,95.395 153.466,97.371 154.242 C 98.172 154.557,116.383 161.308,137.839 169.245 M76.545 321.289 C 76.529 372.360,76.447 375.570,75.074 378.593 C 70.864 387.864,58.456 388.501,52.558 379.749 L 50.781 377.113 50.781 322.150 L 50.781 267.188 63.672 267.188 L 76.563 267.188 76.545 321.289 ""></path>
        </svg>";

        // A
        public static string AlertTriangle => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-alert-triangle"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M10.24 3.957l-8.422 14.06a1.989 1.989 0 0 0 1.7 2.983h16.845a1.989 1.989 0 0 0 1.7 -2.983l-8.423 -14.06a1.989 1.989 0 0 0 -3.4 0z""></path>
           <path d=""M12 9v4""></path>
           <path d=""M12 17h.01""></path>
        </svg>";
        public static string ArrowDown => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-arrow-badge-down-filled"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M16.375 6.22l-4.375 3.498l-4.375 -3.5a1 1 0 0 0 -1.625 .782v6a1 1 0 0 0 .375 .78l5 4a1 1 0 0 0 1.25 0l5 -4a1 1 0 0 0 .375 -.78v-6a1 1 0 0 0 -1.625 -.78z"" stroke-width=""0"" fill=""currentColor""></path>
        </svg>";
        public static string ArrowMoveHorizontal => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-arrows-move-horizontal"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M18 9l3 3l-3 3""></path>
           <path d=""M15 12h6""></path>
           <path d=""M6 9l-3 3l3 3""></path>
           <path d=""M3 12h6""></path>
        </svg>";
        public static string ArrowHorizontal => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-arrows-horizontal"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M7 8l-4 4l4 4""></path>
           <path d=""M17 8l4 4l-4 4""></path>
           <path d=""M3 12l18 0""></path>
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
        public static string RefreshDot => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-refresh-dot"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M20 11a8.1 8.1 0 0 0 -15.5 -2m-.5 -4v4h4""></path>
           <path d=""M4 13a8.1 8.1 0 0 0 15.5 2m.5 4v-4h-4""></path>
           <path d=""M12 12m-1 0a1 1 0 1 0 2 0a1 1 0 1 0 -2 0""></path>
        </svg>";
        // S
        public static string ShieldChevron => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-shield-chevron"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M12 3a12 12 0 0 0 8.5 3a12 12 0 0 1 -8.5 15a12 12 0 0 1 -8.5 -15a12 12 0 0 0 8.5 -3""></path>
           <path d=""M4 14l8 -3l8 3""></path>
        </svg>";
        public static string ShieldFilledCross => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-shield-checkered-filled"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M11.013 12v9.754a13 13 0 0 1 -8.733 -9.754h8.734zm9.284 3.794a13 13 0 0 1 -7.283 5.951l-.001 -9.745h8.708a12.96 12.96 0 0 1 -1.424 3.794zm-9.283 -13.268l-.001 7.474h-8.986c-.068 -1.432 .101 -2.88 .514 -4.282a1 1 0 0 1 1.005 -.717a11 11 0 0 0 7.192 -2.256l.276 -.219zm1.999 7.474v-7.453l-.09 -.073a11 11 0 0 0 7.189 2.537l.342 -.01a1 1 0 0 1 1.005 .717c.413 1.403 .582 2.85 .514 4.282h-8.96z"" stroke-width=""0"" fill=""currentColor""></path>
        </svg>";
        public static string ShieldHalf => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-shield-half-filled"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M12 3a12 12 0 0 0 8.5 3a12 12 0 0 1 -8.5 15a12 12 0 0 1 -8.5 -15a12 12 0 0 0 8.5 -3""></path>
           <path d=""M12 3v18""></path>
           <path d=""M12 11h8.9""></path>
           <path d=""M12 8h8.9""></path>
           <path d=""M12 5h3.1""></path>
           <path d=""M12 17h6.2""></path>
           <path d=""M12 14h8""></path>
        </svg>";
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
        public static string Sword => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-sword"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M20 4v5l-9 7l-4 4l-3 -3l4 -4l7 -9z""></path>
           <path d=""M6.5 11.5l6 6""></path>
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
        public static string Turbine => @"<svg xmlns=""http://www.w3.org/2000/svg"" class=""icon icon-tabler icon-tabler-car-turbine"" width=""24"" height=""24"" viewBox=""0 0 24 24"" stroke-width=""2"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
           <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
           <path d=""M11 13m-4 0a4 4 0 1 0 8 0a4 4 0 1 0 -8 0""></path>
           <path d=""M18.86 11c.088 .66 .14 1.512 .14 2a8 8 0 1 1 -8 -8h6""></path>
           <path d=""M11 9c2.489 .108 4.489 .108 6 0""></path>
           <path d=""M17 3m0 1a1 1 0 0 1 1 -1h2a1 1 0 0 1 1 1v6a1 1 0 0 1 -1 1h-2a1 1 0 0 1 -1 -1z""></path>
           <path d=""M11 13l-3.5 -1.5""></path>
           <path d=""M11 13l2.5 3""></path>
           <path d=""M8.5 16l2.5 -3""></path>
           <path d=""M11 13l3.5 -1.5""></path>
           <path d=""M11 9v4""></path>
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
