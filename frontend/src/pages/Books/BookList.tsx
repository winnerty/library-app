import { useEffect, useState, useMemo } from "react"
import { type Book, getBooks, deleteBook } from "../../api/books"
import BookForm from "./BookForm"
import React from "react"
import { useSearch } from "../../context/SearchContext"

export default function BookList() {
    const [books, setBooks] = useState<Book[]>([])
    const [showForm, setShowForm] = useState(false)
    const { searchQuery } = useSearch()

    const filteredBooks = useMemo(() => {
        return books.filter(book => 
            book.title.toLowerCase().includes(searchQuery.toLowerCase())
        )
    }, [books, searchQuery])

    useEffect(() => {
        getBooks().then(setBooks)
    }, [])

    const handleDelete = async (id: number) => {
        await deleteBook(id)
        setBooks(await getBooks())
    }

    return (
        <div className="space-y-6">
            <div className="flex justify-between items-center">
                <h2 className="text-2xl font-semibold text-gray-800">Books Collection</h2>
                <button
                    onClick={() => setShowForm(true)}
                    className="bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700 transition-colors"
                >
                    + Add Book
                </button>
            </div>

            {showForm && (
                <BookForm
                    onClose={async () => {
                        setShowForm(false);
                        setBooks(await getBooks());
                    }}
                />
            )}

            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                {filteredBooks.map((book) => (
                    <div key={book.id} className="bg-white rounded-xl shadow-md overflow-hidden hover:shadow-lg transition-shadow">
                        <div className="h-48 bg-gradient-to-br from-blue-400 to-indigo-500 flex items-center justify-center">
                            <span className="text-6xl">📚</span>
                        </div>
                        <div className="p-6">
                            <h3 className="text-xl font-semibold text-gray-800 mb-2">{book.title}</h3>
                            <div className="text-gray-600">
                                <p className="mb-1">by {book.author}</p>
                                <p className="text-sm">Published: {book.year}</p>
                            </div>
                            <div className="mt-4 flex gap-2">
                                <button className="flex-1 bg-blue-50 text-blue-600 px-4 py-2 rounded-lg hover:bg-blue-100 transition-colors">
                                    Borrow
                                </button>
                                <button
                                    onClick={() => handleDelete(book.id)}
                                    className="px-4 py-2 text-red-600 hover:text-red-700 transition-colors"
                                    title="Delete book"
                                >
                                    <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
                                    </svg>
                                </button>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    )
}