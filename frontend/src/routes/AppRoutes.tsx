import { Routes, Route, Navigate } from "react-router-dom"
import BookList from "../pages/Books"
import React from "react"
import BorrowerList from "../pages/Borrowers"
import ReservationList from "../pages/Reservations"

export default function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/books" replace />} />
      <Route path="/books" element={<BookList />} />
      <Route path="/borrowers" element={<BorrowerList />} />
      <Route path="/reservations" element={<ReservationList />} />
    </Routes>
  )
}