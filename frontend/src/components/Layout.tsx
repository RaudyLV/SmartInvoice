import { ReactNode } from 'react';
import { useNavigate } from 'react-router-dom';
import NavBar from '@/components/NavBar';
import Footer from '@/components/Footer';

interface LayoutProps {
  children: ReactNode;
}

export default function Layout({ children }: LayoutProps) {
  const navigate = useNavigate();

  const hideNavbar = location.pathname.startsWith('/auth');
  return (
    <div className="relative w-full min-h-screen bg-black"> 
      <div className="relative z-10 flex flex-col min-h-screen">
        {!hideNavbar && <NavBar onNavigate={(path: string) => navigate(path)} />}
        <main className="flex-grow">{children}</main>
        <Footer />
      </div>
    </div>
  );
}

