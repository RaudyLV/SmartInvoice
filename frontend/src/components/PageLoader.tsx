// components/HeroSkeleton.tsx
export default function HeroSkeleton() {
  return (
    <div className="min-h-screen flex items-center justify-center px-4">
      <div className="max-w-6xl w-full">
        <div className="text-center space-y-8">
          {/* Título principal con shimmer */}
          <div className="space-y-4">
            <div className="relative h-16 bg-gray-800 rounded-lg w-3/4 mx-auto overflow-hidden">
              <div className="absolute inset-0 bg-gradient-to-r from-transparent via-gray-700 to-transparent shimmer"></div>
            </div>
            <div className="relative h-16 bg-gray-800 rounded-lg w-2/3 mx-auto overflow-hidden">
              <div className="absolute inset-0 bg-gradient-to-r from-transparent via-gray-700 to-transparent shimmer"></div>
            </div>
          </div>

          {/* Subtítulo */}
          <div className="space-y-3">
            <div className="relative h-6 bg-gray-800 rounded-lg w-5/6 mx-auto overflow-hidden">
              <div className="absolute inset-0 bg-gradient-to-r from-transparent via-gray-700 to-transparent shimmer"></div>
            </div>
            <div className="relative h-6 bg-gray-800 rounded-lg w-4/6 mx-auto overflow-hidden">
              <div className="absolute inset-0 bg-gradient-to-r from-transparent via-gray-700 to-transparent shimmer"></div>
            </div>
          </div>

          {/* Botones */}
          <div className="flex gap-4 justify-center flex-wrap">
            <div className="relative h-14 bg-gray-800 rounded-lg w-40 overflow-hidden">
              <div className="absolute inset-0 bg-gradient-to-r from-transparent via-gray-700 to-transparent shimmer"></div>
            </div>
            <div className="relative h-14 bg-gray-800 rounded-lg w-40 overflow-hidden">
              <div className="absolute inset-0 bg-gradient-to-r from-transparent via-gray-700 to-transparent shimmer"></div>
            </div>
          </div>

          {/* Features (opcional) */}
          <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mt-16">
            {[1, 2, 3].map((i) => (
              <div key={i} className="relative h-32 bg-gray-800 rounded-xl overflow-hidden">
                <div className="absolute inset-0 bg-gradient-to-r from-transparent via-gray-600 to-transparent shimmer"></div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}