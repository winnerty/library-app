export interface Author {
    id: number;
    name: string;
}

const API_BASE_URL = import.meta.env.VITE_API_URL || "http://localhost:5102/api";

export async function getAuthors(): Promise<Author[]> {
    const response = await fetch(`${API_BASE_URL}/author`);
    if (!response.ok) {
        throw new Error('Failed to fetch authors');
    }
    return response.json();
}