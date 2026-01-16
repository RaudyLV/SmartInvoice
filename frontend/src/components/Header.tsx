import { ArrowRight, Sparkles, Zap, Shield } from "lucide-react";

interface HeaderProps {
  title?: string;
  subtitle?: string;
  description?: string;
  showCTA?: boolean;
  onCTAClick?: () => void;
  ctaText?: string;
  variant?: "hero" | "simple" | "minimal";
}

export default function Header({
  title = "Build Something Amazing",
  subtitle = "The Future is Neural",
  description = "Experience the next generation of web applications with AI-powered backgrounds and seamless interactions.",
  showCTA = true,
  onCTAClick,
  ctaText = "Get Started",
  variant = "hero",
}: HeaderProps) {
  if (variant === "minimal") {
    return (
      <header className="relative pt-32 pb-20 px-4">
        <div className="max-w-4xl mx-auto text-center">
          <h1 className="text-5xl md:text-6xl font-bold text-white mb-6 leading-tight">
            {title}
          </h1>
          {description && (
            <p className="text-xl text-white/80 max-w-2xl mx-auto">
              {description}
            </p>
          )}
        </div>
      </header>
    );
  }

  if (variant === "simple") {
    return (
      <header className="relative pt-32 pb-24 px-4">
        <div className="max-w-5xl mx-auto text-center">
          <div className="inline-flex items-center space-x-2 px-4 py-2 rounded-full bg-white/10 backdrop-blur-sm border border-white/20 mb-6">
            <Sparkles size={16} className="text-purple-400" />
            <span className="text-sm text-white/90">{subtitle}</span>
          </div>

          <h1 className="text-5xl md:text-7xl font-bold text-white mb-6 leading-tight bg-clip-text text-transparent bg-gradient-to-r from-white to-white/80">
            {title}
          </h1>

          <p className="text-xl text-white/70 max-w-3xl mx-auto mb-10">
            {description}
          </p>

          {showCTA && (
            <button
              onClick={onCTAClick}
              className="inline-flex items-center space-x-2 px-8 py-4 rounded-full bg-gradient-to-r from-purple-500 to-pink-500 text-white font-semibold hover:from-purple-600 hover:to-pink-600 transform hover:scale-105 transition-all duration-300 shadow-lg hover:shadow-purple-500/50"
            >
              <span>{ctaText}</span>
              <ArrowRight size={20} />
            </button>
          )}
        </div>
      </header>
    );
  }

  // Hero variant (default)
  return (
    <header className="relative pt-32 pb-28 px-4 overflow-hidden">
      {/* Floating elements */}
      <div className="absolute inset-0 overflow-hidden pointer-events-none">
        <div className="absolute top-20 left-10 w-72 h-72 bg-purple-500/20 rounded-full blur-3xl animate-pulse" />
        <div className="absolute bottom-20 right-10 w-96 h-96 bg-pink-500/20 rounded-full blur-3xl animate-pulse delay-1000" />
      </div>

      <div className="max-w-6xl mx-auto relative z-10">
        <div className="text-center">
          {/* Badge */}
          <div className="inline-flex items-center space-x-2 px-4 py-2 rounded-full bg-white/10 backdrop-blur-sm border border-white/20 mb-8 animate-fade-in">
            <Sparkles size={16} className="text-purple-400 animate-spin-slow" />
            <span className="text-sm text-white/90 font-medium">
              {subtitle}
            </span>
            <div className="w-2 h-2 bg-green-400 rounded-full animate-pulse" />
          </div>

          {/* Main Title */}
          <h1 className="text-6xl md:text-8xl font-black text-white mb-6 leading-tight tracking-tight">
            {/* Opción con fallback (texto blanco + gradiente encima): */}
            <span className="text-white bg-gradient-to-r from-purple-400 via-pink-500 to-purple-400 bg-clip-text">
              {title}
            </span>
          </h1>

          {/* Description */}
          <p className="text-xl md:text-2xl text-white/70 max-w-3xl mx-auto mb-12 leading-relaxed">
            {description}
          </p>

          {/* CTA Buttons */}
          {showCTA && (
            <div className="flex flex-col sm:flex-row items-center justify-center gap-4 mb-16">
              <button
                onClick={onCTAClick}
                className="group relative inline-flex items-center space-x-2 px-8 py-4 rounded-full bg-gradient-to-r from-purple-500 to-pink-500 text-white font-semibold hover:from-purple-600 hover:to-pink-600 transform hover:scale-105 transition-all duration-300 shadow-2xl hover:shadow-purple-500/50"
              >
                <span>{ctaText}</span>
                <ArrowRight
                  size={20}
                  className="group-hover:translate-x-1 transition-transform"
                />
              </button>

              <button className="inline-flex items-center space-x-2 px-8 py-4 rounded-full bg-white/10 backdrop-blur-sm border border-white/20 text-white font-semibold hover:bg-white/20 transition-all duration-300">
                <span>Aprende más</span>
              </button>
            </div>
          )}

          {/* Features */}
          <section className="grid grid-cols-1 md:grid-cols-3 gap-6 max-w-4xl mx-auto">
            <div className="group p-6 rounded-2xl bg-white/5 backdrop-blur-sm border border-white/10 hover:bg-white/10 hover:border-purple-500/50 transition-all duration-300 hover:-translate-y-1">
              <div className="w-12 h-12 rounded-xl bg-gradient-to-br from-purple-500 to-pink-500 flex items-center justify-center mb-4 group-hover:scale-110 transition-transform">
                <Zap className="text-white" size={24} />
              </div>
              <h3 className="text-white font-semibold text-lg mb-2">
                Optimizado Rápido
              </h3>
              <p className="text-white/60 text-sm">
                Recursos optimizados para la mejor experiencia de usuario.
              </p>
            </div>

            <div className="group p-6 rounded-2xl bg-white/5 backdrop-blur-sm border border-white/10 hover:bg-white/10 hover:border-purple-500/50 transition-all duration-300 hover:-translate-y-1">
              <div className="w-12 h-12 rounded-xl bg-gradient-to-br from-blue-500 to-cyan-500 flex items-center justify-center mb-4 group-hover:scale-110 transition-transform">
                <Shield className="text-white" size={24} />
              </div>
              <h3 className="text-white font-semibold text-lg mb-2">
                Seguro & Confiable
              </h3>
              <p className="text-white/60 text-sm">
                Seguridad empresarial confiable.
              </p>
            </div>

            <div className="group p-6 rounded-2xl bg-white/5 backdrop-blur-sm border border-white/10 hover:bg-white/10 hover:border-purple-500/50 transition-all duration-300 hover:-translate-y-1">
              <div className="w-12 h-12 rounded-xl bg-gradient-to-br from-orange-500 to-red-500 flex items-center justify-center mb-4 group-hover:scale-110 transition-transform">
                <Sparkles className="text-white" size={24} />
              </div>
              <h3 className="text-white font-semibold text-lg mb-2">
                Potenciado 
              </h3>
              <p className="text-white/60 text-sm">
                Next-gen optimización con IA integrada.                
              </p>
            </div>
          </section>
        </div>
      </div>
    </header>
  );
}
