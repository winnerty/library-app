import { useEffect, useState, useMemo } from "react";
import {
  type Reservation,
  getReservations,
  deleteReservation,
  cancelReservation,
} from "../../api/reservations";
import ReservationForm from "./ReservationForm";
import { useSearch } from "../../context/SearchContext";
import React from "react";

export default function ReservationList() {
  const [reservations, setReservations] = useState<Reservation[]>([]);
  const [showForm, setShowForm] = useState(false);
  const [editingReservation, setEditingReservation] = useState<Reservation | null>(null);
  const { searchQuery } = useSearch();

  const load = async () => setReservations(await getReservations());

  useEffect(() => {
    load();
  }, []);

  const handleDelete = async (id: number) => {
    await deleteReservation(id);
    load();
  };

  const handleCancel = async (id: number) => {
    await cancelReservation(id);
    load();
  };

  const filtered = useMemo(() => {
    const q = searchQuery.toLowerCase();
    return reservations.filter(
      (r) =>
        r.borrowerName.toLowerCase().includes(q) ||
        r.bookTitle.toLowerCase().includes(q)
    );
  }, [reservations, searchQuery]);

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <h2 className="text-2xl font-semibold text-gray-800">Reservations</h2>
        <button
          onClick={() => setShowForm(true)}
          className="bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700"
        >
          + Add Reservation
        </button>
      </div>

      {(showForm || editingReservation) && (
        <ReservationForm
          reservation={editingReservation ?? undefined}
          onClose={() => {
            setShowForm(false);
            setEditingReservation(null);
            load();
          }}
        />
      )}

      {filtered.length === 0 ? (
        <p className="text-gray-600 text-center mt-8">
          No reservations found.
        </p>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {filtered.map((r) => (
            <div
              key={r.id}
              className="bg-white rounded-xl shadow-md p-6 hover:shadow-lg transition"
            >
              <h3 className="text-xl font-semibold mb-2">{r.bookTitle}</h3>
              <p className="text-gray-700">Borrower: {r.borrowerName}</p>
              <p className="text-gray-600 text-sm">
                From: {r.reservationDate.split("T")[0]} — To:{" "}
                {r.expirationDate.split("T")[0]}
              </p>
              <p
                className={`mt-2 font-medium ${
                  r.isActive ? "text-green-600" : "text-gray-400"
                }`}
              >
                {r.isActive ? "Active" : "Cancelled"}
              </p>

              <div className="flex gap-2 mt-4">
                <button
                  onClick={() => setEditingReservation(r)}
                  className="flex-1 bg-blue-50 text-blue-600 px-4 py-2 rounded-lg hover:bg-blue-100"
                >
                  Edit
                </button>
                <button
                  onClick={() => handleCancel(r.id)}
                  disabled={!r.isActive}
                  className={`flex-1 px-4 py-2 rounded-lg border ${
                    r.isActive
                      ? "text-yellow-600 border-yellow-400 hover:bg-yellow-50"
                      : "text-gray-400 border-gray-200 cursor-not-allowed"
                  }`}
                >
                  Cancel
                </button>
                <button
                  onClick={() => handleDelete(r.id)}
                  className="px-4 py-2 text-red-600 hover:text-red-700"
                >
                  Delete
                </button>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}