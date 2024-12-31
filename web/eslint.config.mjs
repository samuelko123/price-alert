import { dirname } from "path";
import { fileURLToPath } from "url";
import { FlatCompat } from "@eslint/eslintrc";
import stylistic from "@stylistic/eslint-plugin";

const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

const compat = new FlatCompat({
  baseDirectory: __dirname,
});

const eslintConfig = [
  ...compat.extends("next/core-web-vitals", "next/typescript"),
  {
    plugins: {
      "@stylistic": stylistic,
    },
    rules: {
      "@stylistic/comma-dangle": ["error", "always-multiline"],
      "@stylistic/quotes": ["error", "double"],
      "@stylistic/semi": ["error", "always"],
      "react/no-unknown-property": ["error", { ignore: ["css"] }],
    },
  },
  {
    // Server-side image fetching is unavailable due to the lack of mutual TLS support:
    // https://github.com/vercel/next.js/discussions/35533
    rules: {
      "@next/next/no-img-element": "off",
    },
  },
];

export default eslintConfig;
