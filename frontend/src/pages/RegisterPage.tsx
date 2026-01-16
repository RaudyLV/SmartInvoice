import { Register } from "@/components/Register";
import { usePageTitle } from "@/hooks/usePageTitle";
import { ArrowBigLeftDashIcon } from "lucide-react";
import { Link } from "react-router-dom";

export default function RegisterPage() {
  usePageTitle("SmartInvoice: sign up");

  return (
    <div className="flex flex-col min-h-screen ">
      <header className="p-4 hidden md:flex">
        <Link 
          to={"/"}
          className="flex items-center gap-2 text-heading font-medium hover:-translate-x-1 transition-transform"
        >
          <ArrowBigLeftDashIcon x={200}/>
          <span> Inicio</span>
        </Link>
      </header>
      <main className="flex-grow flex items-center justify-center p-4 md:p-6 lg:p-8">
        <div className="w-full max-w-md md:max-w-lg lg:max-w-xl">
          <Register />
        </div>
      </main>
    </div>
  );
}
