import { useState } from "react"
import { createBook } from "../../api/books"
import React from "react"

type Props = {
    onClose: () => void
}

export default function BookForm({ onClose }: Props) {
    const [title, setTitle] = useState("")
    const [author, setAuthor] = useState("")
    const [year, setYear] = useState<number>(2020)

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault()
        await createBook({ title, author, year })
        onClose()
    }

    return (
        <div className="bg-white border p-4 rounded mb-4">
            <h3 className="font-semibold mb-2">Add Book</h3>
            <form onSubmit={handleSubmit} className="flex flex-col gap-2">
                <input
                    placeholder="Title"
                    value={title}
                    onChange={e => setTitle(e.target.value)}
                    className="border p-1 rounded"
                />
                <input
                    placeholder="Author"
                    value={author}
                    onChange={e => setAuthor(e.target.value)}
                    className="border p-1 rounded"
                />
                <input
                    type="number"
                    placeholder="Year"
                    value={year}
                    onChange={e => setYear(Number(e.target.value))}
                    className="border p-1 rounded"
                />
                <div className="flex gap-2 mt-2">
                    <button className="bg-green-500 text-white px-3 py-1 rounded" type="submit">
                        Save
                    </button>
                    <button className="border px-3 py-1 rounded" onClick={onClose} type="button">
                        Cancel
                    </button>
                </div>
            </form>
        </div>
    )
}