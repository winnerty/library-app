export interface Borrower {
  id: number;
  name: string;
  email: string;
  phoneNumber: string;
}

export interface CreateBorrowerDTO {
  name: string;
  email: string;
  phoneNumber: string;
}

const API_BASE_URL = import.meta.env.VITE_API_URL || "http://localhost:5102/api";

export async function getBorrowers(): Promise<Borrower[]> {
  const response = await fetch(`${API_BASE_URL}/borrower`);
  if (!response.ok) {
    throw new Error("Failed to fetch borrowers");
  }
  return response.json();
}

export async function getBorrower(id: number): Promise<Borrower> {
  const response = await fetch(`${API_BASE_URL}/borrower/${id}`);
  if (!response.ok) {
    throw new Error("Failed to fetch borrower");
  }
  return response.json();
}

export async function createBorrower(data: CreateBorrowerDTO): Promise<Borrower> {
  const response = await fetch(`${API_BASE_URL}/borrower`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  if (!response.ok) {
    throw new Error("Failed to create borrower");
  }
  return response.json();
}

export async function updateBorrower(id: number, data: CreateBorrowerDTO): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/borrower/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  if (!response.ok) {
    throw new Error("Failed to update borrower");
  }
}

export async function deleteBorrower(id: number): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/borrower/${id}`, {
    method: "DELETE",
  });
  if (!response.ok) {
    throw new Error("Failed to delete borrower");
  }
}