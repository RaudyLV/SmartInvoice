import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Layout from '@/components/Layout';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import Home  from '@/pages/Home'

function App() {
  return (
    <BrowserRouter>
      <Layout>
        <Routes>
          <Route path="/products" element={<div className="text-white p-8">Products Page</div>} />
          <Route path="/auth/signin" element={<LoginPage/>} />
          <Route path="/auth/signup" element={<RegisterPage/>} />
          <Route path="/clients" element={<div className="text-white p-8">Clients Page</div>} />
          <Route path="/dashboard" element={<div className="text-white p-8">Dashboard Page</div>} />
          <Route path="/" element={<Home/>} />
        </Routes>
      </Layout>
    </BrowserRouter>
  );
}

export default App;


// // ============================================
// // DASHBOARD
// // ============================================
// const Dashboard = () => {
//   const { user, logout } = useAuth();

//   return (
//     <div className="min-h-screen bg-gray-50">
//       {/* Header */}
//       <div className="bg-gradient-to-r from-indigo-600 to-purple-600 text-white px-8 py-6 shadow-lg">
//         <div className="max-w-7xl mx-auto flex justify-between items-center">
//           <h1 className="text-3xl font-bold">📊 Dashboard</h1>
//           <div className="flex items-center gap-4">
//             <div className="text-right">
//               <p className="text-sm opacity-90">Bienvenido</p>
//               <p className="font-semibold">{user?.userName || user?.email}</p>
//               {user?.roles && user.roles.length > 0 && (
//                 <p className="text-xs opacity-75">{user.roles.join(", ")}</p>
//               )}
//             </div>
//             <button
//               onClick={logout}
//               className="bg-white/20 hover:bg-white/30 px-6 py-2 rounded-lg font-medium transition-all"
//             >
//               Cerrar Sesión
//             </button>
//           </div>
//         </div>
//       </div>

//       {/* Content */}
//       <div className="max-w-7xl mx-auto p-8">
//         <div className="bg-white rounded-2xl shadow-md p-8 mb-8">
//           <h2 className="text-2xl font-bold text-gray-800 mb-2">
//             ¡Bienvenido a SmartInvoice!
//           </h2>
//           <p className="text-gray-600 mb-6">
//             Tu sistema de facturación está listo para usar.
//           </p>

//           {/* Stats Grid */}
//           <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
//             <div className="bg-gradient-to-br from-blue-50 to-blue-100 rounded-xl p-6 hover:shadow-lg transition-shadow">
//               <div className="flex items-center gap-4">
//                 <div className="text-5xl">📦</div>
//                 <div>
//                   <h3 className="text-3xl font-bold text-gray-800">0</h3>
//                   <p className="text-gray-600 text-sm">Productos</p>
//                 </div>
//               </div>
//             </div>

//             <div className="bg-gradient-to-br from-purple-50 to-purple-100 rounded-xl p-6 hover:shadow-lg transition-shadow">
//               <div className="flex items-center gap-4">
//                 <div className="text-5xl">👥</div>
//                 <div>
//                   <h3 className="text-3xl font-bold text-gray-800">0</h3>
//                   <p className="text-gray-600 text-sm">Clientes</p>
//                 </div>
//               </div>
//             </div>

//             <div className="bg-gradient-to-br from-green-50 to-green-100 rounded-xl p-6 hover:shadow-lg transition-shadow">
//               <div className="flex items-center gap-4">
//                 <div className="text-5xl">📄</div>
//                 <div>
//                   <h3 className="text-3xl font-bold text-gray-800">0</h3>
//                   <p className="text-gray-600 text-sm">Facturas</p>
//                 </div>
//               </div>
//             </div>

//             <div className="bg-gradient-to-br from-yellow-50 to-yellow-100 rounded-xl p-6 hover:shadow-lg transition-shadow">
//               <div className="flex items-center gap-4">
//                 <div className="text-5xl">💰</div>
//                 <div>
//                   <h3 className="text-3xl font-bold text-gray-800">$0.00</h3>
//                   <p className="text-gray-600 text-sm">Total Vendido</p>
//                 </div>
//               </div>
//             </div>
//           </div>
//         </div>
//       </div>
//     </div>
//   );
// };