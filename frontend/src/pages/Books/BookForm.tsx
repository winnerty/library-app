import { useState, useEffect } from "react"
import { createBook, updateBook, type Book } from "../../api/books"
import { getAuthors, type Author } from "../../api/authors"
import React from "react"
import styles from './BookForm.module.css'

type Props = {
    onClose: () => void
    book?: Book
}

export default function BookForm({ onClose, book }: Props) {
    const [title, setTitle] = useState(book?.title ?? "")
    const [authorId, setAuthorId] = useState<number>(book?.authorId ?? 0)
    const [publicationYear, setPublicationYear] = useState<number>(book?.publicationYear ?? new Date().getFullYear())
    const [authors, setAuthors] = useState<Author[]>([])

    useEffect(() => {
        getAuthors().then(setAuthors)
    }, [])

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault()
        try {
            if (authorId === 0) {
                alert('Please select an author')
                return
            }
            
            const bookData = { 
                title, 
                authorId, 
                publicationYear 
            }

            if (book) {
                await updateBook(book.id, bookData)
            } else {
                await createBook(bookData)
            }
            
            onClose()
        } catch (error) {
            console.error('Error saving book:', error)
            alert('Failed to save the book. Please try again.')
        }
    }

    return (
        <div className={styles.overlay}>
            <div className={styles.modal}>
                <div className={styles.header}>
                    <h3 className={styles.title}>{book ? 'Edit Book' : 'Add New Book'}</h3>
                    <button 
                        onClick={onClose}
                        className={styles.closeButton}
                    >
                        <svg width="24" height="24" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M6 18L18 6M6 6l12 12" />
                        </svg>
                    </button>
                </div>
                <form onSubmit={handleSubmit} className={styles.form}>
                    <div className={styles.formGroup}>
                        <label className={styles.label}>
                            Book Title
                        </label>
                        <input
                            placeholder="Enter book title"
                            value={title}
                            onChange={e => setTitle(e.target.value)}
                            className={styles.input}
                            required
                        />
                    </div>
                    <div className={styles.formGroup}>
                        <label className={styles.label}>
                            Author
                        </label>
                        <select
                            value={authorId}
                            onChange={e => setAuthorId(Number(e.target.value))}
                            className={styles.input}
                            required
                        >
                            <option value={0}>Select an author</option>
                            {authors.map(author => (
                                <option key={author.id} value={author.id}>
                                    {author.name}
                                </option>
                            ))}
                        </select>
                    </div>
                    <div className={styles.formGroup}>
                        <label className={styles.label}>
                            Publication Year
                        </label>
                        <input
                            type="number"
                            placeholder="Enter publication year"
                            value={publicationYear}
                            onChange={e => setPublicationYear(Number(e.target.value))}
                            className={styles.input}
                            required
                            min={1600}
                            max={2025}
                        />
                    </div>
                    <div className={styles.buttons}>
                        <button 
                            type="button" 
                            onClick={onClose}
                            className={`${styles.button} ${styles.cancelButton}`}
                        >
                            Cancel
                        </button>
                        <button 
                            type="submit"
                            className={`${styles.button} ${styles.saveButton}`}
                        >
                            Save Book
                        </button>
                    </div>
                </form>
            </div>
        </div>
    )
}