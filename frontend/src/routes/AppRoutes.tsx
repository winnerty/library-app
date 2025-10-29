import { Routes, Route, Navigate } from "react-router-dom"
import BookList from "../pages/Books"
import Borrowers from "../pages/Borrowers"
import Reservations from "../pages/Reservations"
import React from "react"

export default function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/books" replace />} />
      <Route path="/books" element={<BookList />} />
      <Route path="/borrowers" element={<Borrowers />} />
      <Route path="/reservations" element={<Reservations />} />
    </Routes>
  )
}