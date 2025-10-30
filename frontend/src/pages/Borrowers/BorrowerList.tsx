import { useEffect, useState, useMemo } from "react";
import { type Borrower, getBorrowers, deleteBorrower } from "../../api/borrowers";
import BorrowerForm from "./BorrowerForm";
import { useSearch } from "../../context/SearchContext";
import React from "react";

export default function BorrowerList() {
  const [borrowers, setBorrowers] = useState<Borrower[]>([]);
  const [showForm, setShowForm] = useState(false);
  const [editingBorrower, setEditingBorrower] = useState<Borrower | null>(null);
  const { searchQuery } = useSearch();

  const load = async () => setBorrowers(await getBorrowers());

  useEffect(() => {
    load();
  }, []);

  const handleDelete = async (id: number) => {
    await deleteBorrower(id);
    load();
  };

  const filteredBorrowers = useMemo(() => {
    return borrowers.filter(b =>
      b.name.toLowerCase().includes(searchQuery.toLowerCase())
    );
  }, [borrowers, searchQuery]);

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <h2 className="text-2xl font-semibold text-gray-800">Borrowers</h2>
        <button
          onClick={() => setShowForm(true)}
          className="bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700 transition"
        >
          + Add Borrower
        </button>
      </div>

      {(showForm || editingBorrower) && (
        <BorrowerForm
          borrower={editingBorrower ?? undefined}
          onClose={() => {
            setShowForm(false);
            setEditingBorrower(null);
            load();
          }}
        />
      )}

      {filteredBorrowers.length === 0 ? (
        <p className="text-gray-600 text-center mt-8">No borrowers found.</p>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {filteredBorrowers.map(b => (
            <div
              key={b.id}
              className="bg-white rounded-xl shadow-md p-6 hover:shadow-lg transition"
            >
                <div className="flex items-center gap-4 mb-4">
                    <img
                    src="user.jpg"
                    alt="User avatar"
                    className="w-12 h-12 rounded-full object-cover"
                    />
                    <h3 className="text-xl font-semibold">{b.name}</h3>
                </div> 
                <p className="text-gray-600">{b.email}</p>
                <p className="text-gray-500">{b.phoneNumber}</p>
                <div className="flex gap-2 mt-4">
                <button
                  onClick={() => setEditingBorrower(b)}
                  className="flex-1 bg-blue-50 text-blue-600 px-4 py-2 rounded-lg hover:bg-blue-100"
                >
                  Edit
                </button>
                <button
                  onClick={() => handleDelete(b.id)}
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