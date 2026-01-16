import { useAuth } from "@/hooks/useAuth";
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { getPasswordStrength, validatePassword } from "@/utils/helpers";
import {
  Mail,
  AlertTriangle,
  Loader2,
  User,
  ArrowRight,
  Lock,
  EyeOff,
  Eye,
  CheckCircle,
  Sparkles,
} from "lucide-react";

export const Register = () => {
  const [userName, setUserName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");
  const [acceptTerms, setAcceptTerms] = useState(false);
  const { register, loading } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async () => {
    setError("");
    setSuccess("");

    if (!userName || !email || !password || !confirmPassword) {
      setError("Por favor completa todos los campos");
      return;
    }

    const passwordError = validatePassword(password);
    if (passwordError) {
      setError(passwordError);
      return;
    }

    if (password !== confirmPassword) {
      setError("Las contraseñas no coinciden");
      return;
    }

    if (!acceptTerms) {
      setError("Debes aceptar los términos y condiciones");
      return;
    }

    try {
      await register(userName, email, password, confirmPassword);
      navigate('/auth/signin');
      setSuccess("¡Cuenta creada exitosamente! Redirigiendo al login...");
      
    } catch (err) {
      setError(err instanceof Error ? err.message : "Error al registrarse");
    }
  };
  const handleKeyPress = (e: React.KeyboardEvent) => {
    if (e.key === "Enter") handleSubmit();
  };
  const strength = getPasswordStrength(password);
  return (
    <div className="relative w-full p-4 md:p-6 lg:p-8">
      {/* Animated Background */}
      <div className="fixed inset-0 bg-black overflow-hidden -z-10">
        <div className="absolute inset-0 bg-[radial-gradient(ellipse_80%_80%_at_50%_-20%,rgba(120,119,198,0.3),rgba(255,255,255,0))]" />
        <div className="absolute top-1/4 left-1/4 w-64 md:w-96 h-64 md:h-96 bg-purple-500/20 rounded-full blur-3xl animate-pulse" />
        <div
          className="absolute bottom-1/4 right-1/4 w-64 md:w-96 h-64 md:h-96 bg-pink-500/20 rounded-full blur-3xl animate-pulse"
          style={{ animationDelay: "1s" }}
        />
      </div>

      {/* Signup Card */}
      <div className="relative z-10 animate-fadeInScale max-w-md md:max-w-lg mx-auto ">
        {/* Glassmorphism Card */}
        <div className="bg-white/10 backdrop-blur-xl rounded-3xl shadow-2xl border border-white/20 p-6 md:p-8 lg:p-10">
          {/* Header */}
          <div className="text-center mb-6 md:mb-8">
            <div className="inline-flex items-center justify-center w-14 h-14 md:w-16 md:h-16 bg-gradient-to-br from-purple-500 to-pink-500 rounded-2xl mb-4 animate-pulse">
              <Sparkles className="text-white" size={28} />
            </div>
            <h1 className="text-3xl md:text-4xl font-bold text-white mb-2">
              SmartInvoice
            </h1>
            <p className="text-white/60 text-sm md:text-base">
              Sistema de Facturación Inteligente
            </p>
          </div>

          <h2 className="text-2xl md:text-3xl font-bold text-white text-center mb-6">
            Crear tu cuenta
          </h2>

          {/* Error Alert */}
          {error && (
            <div className="bg-red-500/20 backdrop-blur-sm border border-red-500/50 text-red-200 px-4 py-3 rounded-xl mb-4 flex items-center gap-3 animate-shake">
              <AlertTriangle size={20} className="flex-shrink-0" />
              <span className="text-sm md:text-base">{error}</span>
            </div>
          )}

          {/* Success Alert */}
          {success && (
            <div className="bg-green-500/20 backdrop-blur-sm border border-green-500/50 text-green-200 px-4 py-3 rounded-xl mb-4 flex items-center gap-3">
              <CheckCircle size={20} className="flex-shrink-0" />
              <span className="text-sm md:text-base">{success}</span>
            </div>
          )}

          {/* Form */}
          <div className="space-y-4 md:space-y-5">
            {/* Username Input */}
            <div>
              <label className="block text-sm font-medium text-white/80 mb-2">
                Nombre de usuario
              </label>
              <div className="relative group">
                <div className="absolute left-4 top-1/2 -translate-y-1/2 text-white/60 group-focus-within:text-purple-400 transition-colors">
                  <User size={20} />
                </div>
                <input
                  type="text"
                  value={userName}
                  onChange={(e) => setUserName(e.target.value)}
                  onKeyDown={handleKeyPress}
                  placeholder="tu_usuario"
                  disabled={loading}
                  className="w-full pl-12 pr-4 py-3 bg-white/5 border-2 border-white/10 rounded-xl text-white placeholder:text-white/40 focus:border-purple-500 focus:bg-white/10 transition-all outline-none disabled:opacity-50 disabled:cursor-not-allowed"
                />
              </div>
            </div>

            {/* Email Input */}
            <div>
              <label className="block text-sm font-medium text-white/80 mb-2">
                Email
              </label>
              <div className="relative group">
                <div className="absolute left-4 top-1/2 -translate-y-1/2 text-white/60 group-focus-within:text-purple-400 transition-colors">
                  <Mail size={20} />
                </div>
                <input
                  type="email"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  onKeyDown={handleKeyPress}
                  placeholder="tu@email.com"
                  disabled={loading}
                  className="w-full pl-12 pr-4 py-3 bg-white/5 border-2 border-white/10 rounded-xl text-white placeholder:text-white/40 focus:border-purple-500 focus:bg-white/10 transition-all outline-none disabled:opacity-50 disabled:cursor-not-allowed"
                />
              </div>
            </div>

            {/* Password Input */}
            <div>
              <label className="block text-sm font-medium text-white/80 mb-2">
                Contraseña
              </label>
              <div className="relative group">
                <div className="absolute left-4 top-1/2 -translate-y-1/2 text-white/60 group-focus-within:text-purple-400 transition-colors">
                  <Lock size={20} />
                </div>
                <input
                  type={showPassword ? "text" : "password"}
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  onKeyDown={handleKeyPress}
                  placeholder="••••••••"
                  disabled={loading}
                  className="w-full pl-12 pr-12 py-3 bg-white/5 border-2 border-white/10 rounded-xl text-white placeholder:text-white/40 focus:border-purple-500 focus:bg-white/10 transition-all outline-none disabled:opacity-50 disabled:cursor-not-allowed"
                />
                <button
                  type="button"
                  onClick={() => setShowPassword(!showPassword)}
                  className="absolute right-4 top-1/2 -translate-y-1/2 text-white/60 hover:text-white transition-colors"
                >
                  {showPassword ? <EyeOff size={20} /> : <Eye size={20} />}
                </button>
              </div>
              {password && (
                <div className="mt-2 flex items-center gap-2">
                  <div className="flex-1 bg-white/10 rounded-full h-1.5">
                    <div
                      className={`h-full rounded-full transition-all ${strength.color}`}
                      style={{
                        width:
                          strength.level === "strong"
                            ? "100%"
                            : strength.level === "medium"
                            ? "66%"
                            : "33%",
                      }}
                    />
                  </div>
                  <span className="text-xs font-medium text-white/80">
                    {strength.text}
                  </span>
                </div>
              )}
            </div>

            {/* Confirm Password Input */}
            <div>
              <label className="block text-sm font-medium text-white/80 mb-2">
                Confirmar Contraseña
              </label>
              <div className="relative group">
                <div className="absolute left-4 top-1/2 -translate-y-1/2 text-white/60 group-focus-within:text-purple-400 transition-colors">
                  <Lock size={20} />
                </div>
                <input
                  type={showPassword ? "text" : "password"}
                  value={confirmPassword}
                  onChange={(e) => setConfirmPassword(e.target.value)}
                  onKeyDown={handleKeyPress}
                  placeholder="••••••••"
                  disabled={loading}
                  className="w-full pl-12 pr-4 py-3 bg-white/5 border-2 border-white/10 rounded-xl text-white placeholder:text-white/40 focus:border-purple-500 focus:bg-white/10 transition-all outline-none disabled:opacity-50 disabled:cursor-not-allowed"
                />
              </div>
            </div>

            {/* Terms Checkbox */}
            <label className="flex items-start gap-3 cursor-pointer group">
              <input
                type="checkbox"
                checked={acceptTerms}
                onChange={(e) => setAcceptTerms(e.target.checked)}
                className="w-4 h-4 mt-1 rounded border-2 border-white/20 bg-white/5 checked:bg-purple-500 checked:border-purple-500 cursor-pointer transition-all"
              />
              <span className="text-sm text-white/80 group-hover:text-white transition-colors">
                Acepto los{" "}
                <button
                  type="button"
                  className="text-purple-300 hover:text-purple-200 font-medium underline"
                >
                  términos y condiciones
                </button>
              </span>
            </label>

            {/* Submit Button */}
            <button
              onClick={handleSubmit}
              disabled={loading}
              className="group w-full bg-gradient-to-r from-purple-500 to-pink-500 text-white py-3.5 rounded-xl font-semibold hover:from-purple-600 hover:to-pink-600 disabled:opacity-50 disabled:cursor-not-allowed transition-all transform hover:scale-[1.02] active:scale-[0.98] flex items-center justify-center gap-2 shadow-lg hover:shadow-purple-500/50 disabled:hover:scale-100"
            >
              {loading ? (
                <>
                  <Loader2 className="animate-spin" size={20} />
                  <span>Creando cuenta...</span>
                </>
              ) : (
                <>
                  <span>Crear Cuenta</span>
                  <ArrowRight
                    size={20}
                    className="group-hover:translate-x-1 transition-transform"
                  />
                </>
              )}
            </button>
          </div>

          {/* Sign In Link */}
          <p className="text-center text-white/60 text-sm mt-6">
            ¿Ya tienes cuenta?{" "}
            <Link
              to="/auth/signin"
              className="text-purple-300 hover:text-purple-200 font-semibold transition-colors"
            >
              Iniciar sesión
            </Link>
          </p>
        </div>
        <div className="absolute -top-10 -left-10 w-20 h-20 bg-purple-500/30 rounded-full blur-2xl" />
        <div className="absolute -bottom-10 -right-10 w-32 h-32 bg-pink-500/30 rounded-full blur-2xl" />
      </div>

      <style>{`
        @keyframes shake {
          0%, 100% { transform: translateX(0); }
          25% { transform: translateX(-10px); }
          75% { transform: translateX(10px); }
        }
        
        .animate-shake {
          animation: shake 0.5s ease-in-out;
        }

        /* Custom scrollbar for the form */
        .custom-scrollbar::-webkit-scrollbar {
          width: 6px;
        }
        
        .custom-scrollbar::-webkit-scrollbar-track {
          background: rgba(255, 255, 255, 0.05);
          border-radius: 10px;
        }
        
        .custom-scrollbar::-webkit-scrollbar-thumb {
          background: rgba(168, 85, 247, 0.5);
          border-radius: 10px;
        }
        
        .custom-scrollbar::-webkit-scrollbar-thumb:hover {
          background: rgba(168, 85, 247, 0.7);
        }
      `}</style>
    </div>
  );
};
