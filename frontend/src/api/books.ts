export interface Book {
    id: number;
    title: string;
    publicationYear: number;
    authorId: number;
    authorName: string;
}

export interface CreateBookDTO {
    title: string;
    publicationYear: number;
    authorId: number;
}

const API_BASE_URL = import.meta.env.VITE_API_URL || "http://localhost:5102/api";

export async function getBooks(): Promise<Book[]> {
    const response = await fetch(`${API_BASE_URL}/book`);
    if (!response.ok) {
        throw new Error('Failed to fetch books');
    }
    return response.json();
}

export async function createBook(book: CreateBookDTO): Promise<Book> {
    const response = await fetch(`${API_BASE_URL}/book`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(book),
    });
    if (!response.ok) {
        throw new Error('Failed to create book');
    }
    return response.json();
}

export async function updateBook(id: number, book: CreateBookDTO): Promise<Book> {
    const response = await fetch(`${API_BASE_URL}/book/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(book),
    });
    if (!response.ok) {
        throw new Error('Failed to update book');
    }
    return response.json();
}

export async function deleteBook(id: number): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/book/${id}`, {
        method: 'DELETE',
    });
    if (!response.ok) {
        throw new Error('Failed to delete book');
    }
}