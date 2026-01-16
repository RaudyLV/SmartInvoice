import { useEffect } from "react";

export function usePageTitle(title: string){
    useEffect(() => {
        const prevTitle = document.title;
        document.title = `${title} - SmartInvoice`

        return () => {
            document.title = prevTitle;
        }
    }, [title]);
}