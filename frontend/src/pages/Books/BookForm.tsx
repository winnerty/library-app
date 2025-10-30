import { useState } from "react"
import { createBook } from "../../api/books"
import React from "react"
import styles from './BookForm.module.css'

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
        <div className={styles.overlay}>
            <div className={styles.modal}>
                <div className={styles.header}>
                    <h3 className={styles.title}>Add New Book</h3>
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
                        <input
                            placeholder="Enter author name"
                            value={author}
                            onChange={e => setAuthor(e.target.value)}
                            className={styles.input}
                            required
                        />
                    </div>
                    <div className={styles.formGroup}>
                        <label className={styles.label}>
                            Publication Year
                        </label>
                        <input
                            type="number"
                            placeholder="Enter publication year"
                            value={year}
                            onChange={e => setYear(Number(e.target.value))}
                            className={styles.input}
                            required
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