export function capitalize(str) {
    if (!str) return '';
    return str.charAt(0).toUpperCase() + str.slice(1);
};

export function formatDate(date: string | Date): string{
    return new Date(date).toLocaleDateString("es-ES",{
        day: "2-digit",
        month: "2-digit",
        year: "numeric"
    });
}

export function formatCurrency(amount: number): string {
    return amount.toLocaleString("es-DO",{
        style: "currency",
        currency: "DOP"
    });
}

export function isEmpty(value: any) {
    return value === null || value === undefined || value === "";
}

export function hasRole(userRoles: string[], required: string) {
    return userRoles.includes(required);
}

export function withLoading<T>(
    callback: () => Promise<T>,
    setLoading: (v: boolean) => void
) {
    return async () => {
        try {
            setLoading(true);
            return await callback();
        } finally {
            setLoading(false);
        }
    };
}

export function getPasswordStrength (pwd: string) {
    if (
      pwd.length >= 8 &&
      /[A-Z]/.test(pwd) &&
      /[a-z]/.test(pwd) &&
      /[0-9]/.test(pwd)
    ) {
      return { level: "strong", text: "Fuerte", color: "bg-green-500" };
    }
    if (pwd.length >= 4) {
      return { level: "medium", text: "Media", color: "bg-yellow-500" };
    }
    return { level: "weak", text: "Débil", color: "bg-red-500" };
  };

 export function validatePassword (pwd: string) {
    if (pwd.length < 8) return "La contraseña debe tener al menos 8 caracteres";
    if (!/[A-Z]/.test(pwd)) return "Debe contener al menos una mayúscula";
    if (!/[a-z]/.test(pwd)) return "Debe contener al menos una minúscula";
    if (!/[0-9]/.test(pwd)) return "Debe contener al menos un número";
    return null;
  };