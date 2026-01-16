import { useState, useEffect } from "react";

export function useDebounce(value: string, delay: number = 500){
    const [debounceValue, setDebounceValue] = useState<string>(value)
    useEffect(() => {
        const handle = setTimeout(() =>{
            setDebounceValue(value);
        }, delay);

        return () => {
            clearTimeout(handle);
        }
    },[value, delay]);

    return debounceValue;
}
