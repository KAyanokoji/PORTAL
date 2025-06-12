/** @type {import('tailwindcss').Config} */
export const content = [
    "./app/**/*.{js,ts,jsx,tsx}",
    "./pages/**/*.{js,ts,jsx,tsx}",
    "./components/**/*.{js,ts,jsx,tsx}",
    "./src/**/*.{js,ts,jsx,tsx}", // if you're using src/ directory
];
export const theme = {
    extend: {},
};
export const plugins = [require("tailwind-scrollbar")];
  