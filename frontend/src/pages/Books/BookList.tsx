import { useEffect, useState } from "react"
import { type Book, getBooks, deleteBook } from "../../api/books"
import BookForm from "./BookForm"
import React from "react"

export default function BookList() {
    const [books, setBooks] = useState<Book[]>([])
    const [showForm, setShowForm] = useState(false)

    useEffect(() => {
        getBooks().then(setBooks)
    }, [])

    const handleDelete = async (id: number) => {
        await deleteBook(id)
        setBooks(await getBooks())
    }

    return (
        <div className="p-4">
            <div className="flex justify-between items-center mb-4">
                <h2 className="text-xl font-semibold">Books</h2>
                <button
                    onClick={() => setShowForm(true)}
                    className="bg-blue-500 text-white px-3 py-1 rounded"
                >
                    + Add Book
                </button>
            </div>

            {showForm && (
                <BookForm
                    onClose={async () => {
                        setShowForm(false)
                        setBooks(await getBooks())
                    }}
                />
            )}

            <table className="w-full border">
                <thead className="bg-gray-100">
                    <tr>
                        <th className="border p-2">Title</th>
                        <th className="border p-2">Author</th>
                        <th className="border p-2">Year</th>
                        <th className="border p-2">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {books.map(b => (
                        <tr key={b.id}>
                            <td className="border p-2">{b.title}</td>
                            <td className="border p-2">{b.author}</td>
                            <td className="border p-2">{b.year}</td>
                            <td className="border p-2 text-center">
                                <button
                                    onClick={() => handleDelete(b.id)}
                                    className="text-red-500"
                                >
                                    Delete
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}