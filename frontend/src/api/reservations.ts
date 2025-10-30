export interface Reservation {
  id: number;
  reservationDate: string;
  expirationDate: string;
  borrowerId: number;
  borrowerName: string;
  bookId: number;
  bookTitle: string;
  isActive: boolean;
}

export interface CreateReservationDTO {
  reservationDate: string;
  expirationDate: string;
  borrowerId: number;
  bookId: number;
}

const API_BASE_URL = import.meta.env.VITE_API_URL || "http://localhost:5102/api";

export async function getReservations(): Promise<Reservation[]> {
  const res = await fetch(`${API_BASE_URL}/reservation`);
  if (!res.ok) throw new Error("Failed to fetch reservations");
  return res.json();
}

export async function createReservation(data: CreateReservationDTO): Promise<Reservation> {
  const res = await fetch(`${API_BASE_URL}/reservation`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  if (!res.ok) throw new Error("Failed to create reservation");
  return res.json();
}

export async function updateReservation(id: number, data: CreateReservationDTO): Promise<void> {
  const res = await fetch(`${API_BASE_URL}/reservation/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  if (!res.ok) throw new Error("Failed to update reservation");
}

export async function deleteReservation(id: number): Promise<void> {
  const res = await fetch(`${API_BASE_URL}/reservation/${id}`, { method: "DELETE" });
  if (!res.ok) throw new Error("Failed to delete reservation");
}

export async function cancelReservation(id: number): Promise<void> {
  const res = await fetch(`${API_BASE_URL}/reservation/${id}/cancel`, { method: "PATCH" });
  if (!res.ok) throw new Error("Failed to cancel reservation");
}