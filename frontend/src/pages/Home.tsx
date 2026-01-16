import { usePageTitle } from '@/hooks/usePageTitle';
import Header from '../components/Header';
import { useNavigate } from 'react-router-dom';
import { useNavigationLoader } from '@/hooks/useNavigationLoader';
import HeroSkeleton from '@/components/PageLoader';

export default function Home() {
  const navigate = useNavigate();
  usePageTitle("Home");
  const isLoading = useNavigationLoader();

  if(isLoading){
    return <HeroSkeleton/>
  }

  return (
    <div className="min-h-screen animate-fadeInScale">
      <Header 
        variant="hero"
        title='Smart Invoice'
        subtitle="🚀 El futuro de facturación"
        description="Experimenta el futuro web con SmartInvoice."
        showCTA={true}
        ctaText="Empezar ahora"
        onCTAClick={() => navigate('/auth/signup')}
      />

    </div>
  );
}