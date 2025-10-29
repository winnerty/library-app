export type Book = {
    id: number
    title: string
    author: string
    year: number
}

let books: Book[] = [
    { id: 1, title: "The Hobbit", author: "J.R.R. Tolkien", year: 1937 },
    { id: 2, title: "1984", author: "George Orwell", year: 1949 },
]

export async function getBooks(): Promise<Book[]> {
    return Promise.resolve(books)
}

export async function createBook(book: Omit<Book, "id">): Promise<Book> {
    const newBook = { ...book, id: Date.now() }
    books.push(newBook)
    return Promise.resolve(newBook)
}

export async function deleteBook(id: number): Promise<void> {
    books = books.filter(b => b.id !== id)
    return Promise.resolve()
}