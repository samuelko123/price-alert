import type { Config } from "tailwindcss";

export default {
  content: [
    "./src/pages/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/components/**/*.{js,ts,jsx,tsx,mdx}",
    "./src/app/**/*.{js,ts,jsx,tsx,mdx}",
  ],
  theme: {
    extend: {
      colors: {
        background: "var(--background)",
        foreground: "var(--foreground)",
        shadow: "var(--shadow)",
        surface: "var(--surface)",
      },
      maxWidth: {
        "sm": "48rem",
      },
    },
  },
  plugins: [],
} satisfies Config;
