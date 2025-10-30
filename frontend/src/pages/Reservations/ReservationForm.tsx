import { useState, useEffect } from "react";
import { createReservation, updateReservation, type Reservation } from "../../api/reservations";
import { getBorrowers, type Borrower } from "../../api/borrowers";
import { getBooks, type Book } from "../../api/books";
import styles from "./ReservationForm.module.css";
import React from "react";

type Props = {
  onClose: () => void;
  reservation?: Reservation;
};

export default function ReservationForm({ onClose, reservation }: Props) {
  const [borrowerId, setBorrowerId] = useState(reservation?.borrowerId ?? 0);
  const [bookId, setBookId] = useState(reservation?.bookId ?? 0);
  const [reservationDate, setReservationDate] = useState(
    reservation?.reservationDate ?? new Date().toISOString().split("T")[0]
  );
  const [expirationDate, setExpirationDate] = useState(
    reservation?.expirationDate ?? new Date().toISOString().split("T")[0]
  );
  const [borrowers, setBorrowers] = useState<Borrower[]>([]);
  const [books, setBooks] = useState<Book[]>([]);

  useEffect(() => {
    getBorrowers().then(setBorrowers);
    getBooks().then(setBooks);
  }, []);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const dto = { borrowerId, bookId, reservationDate, expirationDate };
      if (reservation) await updateReservation(reservation.id, dto);
      else await createReservation(dto);
      onClose();
    } catch (err) {
      console.error("Error saving reservation:", err);
      alert("Failed to save reservation.");
    }
  };

  return (
    <div className={styles.overlay}>
      <div className={styles.modal}>
        <div className={styles.header}>
          <h3 className={styles.title}>
            {reservation ? "Edit Reservation" : "Add Reservation"}
          </h3>
          <button onClick={onClose} className={styles.closeButton}>✕</button>
        </div>

        <form onSubmit={handleSubmit} className={styles.form}>
          <div className={styles.formGroup}>
            <label className={styles.label}>Borrower</label>
            <select
              value={borrowerId}
              onChange={e => setBorrowerId(Number(e.target.value))}
              className={styles.input}
              required
            >
              <option value={0}>Select borrower</option>
              {borrowers.map(b => (
                <option key={b.id} value={b.id}>{b.name}</option>
              ))}
            </select>
          </div>

          <div className={styles.formGroup}>
            <label className={styles.label}>Book</label>
            <select
              value={bookId}
              onChange={e => setBookId(Number(e.target.value))}
              className={styles.input}
              required
            >
              <option value={0}>Select book</option>
              {books.map(b => (
                <option key={b.id} value={b.id}>{b.title}</option>
              ))}
            </select>
          </div>

          <div className={styles.formGroup}>
            <label className={styles.label}>Reservation Date</label>
            <input
              type="date"
              value={reservationDate.split("T")[0]}
              onChange={e => setReservationDate(e.target.value)}
              className={styles.input}
              required
            />
          </div>

          <div className={styles.formGroup}>
            <label className={styles.label}>Expiration Date</label>
            <input
              type="date"
              value={expirationDate.split("T")[0]}
              onChange={e => setExpirationDate(e.target.value)}
              className={styles.input}
              required
            />
          </div>

          <div className={styles.buttons}>
            <button type="button" onClick={onClose}
              className={`${styles.button} ${styles.cancelButton}`}>Cancel</button>
            <button type="submit"
              className={`${styles.button} ${styles.saveButton}`}>Save</button>
          </div>
        </form>
      </div>
    </div>
  );
}