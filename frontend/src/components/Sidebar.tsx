import React from "react"
import { NavLink } from "react-router-dom"

export default function Sidebar() {
    const linkClass = ({ isActive }: { isActive: boolean }) =>
        `block px-4 py-2 rounded ${isActive ? "bg-blue-500 text-white" : "hover:bg-gray-100"}`

    return (
        <div className="w-48 bg-gray-50 h-screen p-4">
            <h1 className="font-bold text-lg mb-4">Library Manager</h1>
            <nav className="flex flex-col gap-1">
                <NavLink to="/books" className={linkClass}>
                    📚 Books
                </NavLink>
                <NavLink to="/borrowers" className={linkClass}>
                    👤 Borrowers
                </NavLink>
                <NavLink to="/reservations" className={linkClass}>
                    � Reservations
                </NavLink>
            </nav>
        </div>
    )
}