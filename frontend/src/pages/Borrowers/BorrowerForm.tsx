import { useState } from "react";
import { createBorrower, updateBorrower, type Borrower } from "../../api/borrowers";
import styles from "./BorrowerForm.module.css";
import React from "react";

type Props = {
  onClose: () => void;
  borrower?: Borrower;
};

export default function BorrowerForm({ onClose, borrower }: Props) {
  const [name, setName] = useState(borrower?.name ?? "");
  const [email, setEmail] = useState(borrower?.email ?? "");
  const [phoneNumber, setPhoneNumber] = useState(borrower?.phoneNumber ?? "");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const data = { name, email, phoneNumber };
      if (borrower) {
        await updateBorrower(borrower.id, data);
      } else {
        await createBorrower(data);
      }
      onClose();
    } catch (err) {
      console.error("Error saving borrower:", err);
      alert("Failed to save borrower. Please try again.");
    }
  };

  return (
    <div className={styles.overlay}>
      <div className={styles.modal}>
        <div className={styles.header}>
          <h3 className={styles.title}>{borrower ? "Edit Borrower" : "Add Borrower"}</h3>
          <button onClick={onClose} className={styles.closeButton}>
            ✕
          </button>
        </div>
        <form onSubmit={handleSubmit} className={styles.form}>
          <div className={styles.formGroup}>
            <label className={styles.label}>Name</label>
            <input
              value={name}
              onChange={e => setName(e.target.value)}
              className={styles.input}
              required
              maxLength={30}
            />
          </div>
          <div className={styles.formGroup}>
            <label className={styles.label}>Email</label>
            <input
              type="email"
              value={email}
              onChange={e => setEmail(e.target.value)}
              className={styles.input}
              required
            />
          </div>
          <div className={styles.formGroup}>
            <label className={styles.label}>Phone Number</label>
            <input
              type="tel"
              value={phoneNumber}
              onChange={e => setPhoneNumber(e.target.value)}
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
            <button type="submit" className={`${styles.button} ${styles.saveButton}`}>
              Save
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}