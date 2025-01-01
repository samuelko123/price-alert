import Image from "next/image";
import Link from "next/link";

import Logo from "@/public/logo.png";

export const Header = () => {
  return (
    <header className="bg-surface px-4 py-2 flex flex-wrap justify-between items-center">
      <Link href="/" className="flex items-center gap-1">
        <Image src={Logo} width="40" height="40" alt="App Logo" />
        <span className="text-xl font-semibold whitespace-nowrap">Price Checker</span>
      </Link>
    </header>
  );
};
