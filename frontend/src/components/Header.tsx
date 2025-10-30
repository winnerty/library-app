import React from "react";
import { NavLink } from "react-router-dom";
import { useSearch } from "../context/SearchContext";

export default function Header() {
    const { searchQuery, setSearchQuery } = useSearch();

    const handleSearch = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchQuery(e.target.value);
    };

    const linkStyle = ({ isActive }: { isActive: boolean }) => 
        `px-4 py-2 rounded-lg transition-colors ${
            isActive 
                ? "bg-white/10 text-white font-medium" 
                : "text-white/80 hover:text-white hover:bg-white/5"
        }`;

    return (
        <header className="bg-gradient-to-r from-blue-600 to-indigo-600 text-white shadow-lg">
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4">
                <div className="flex items-center justify-between">
                    <div className="text-2xl font-bold tracking-wide">
                        📚 Library Manager
                    </div>
                    <div className="flex-1 max-w-2xl mx-8">
                        <div className="relative">
                            <input
                                type="search"
                                placeholder="Search..."
                                value={searchQuery}
                                onChange={handleSearch}
                                className="w-full px-4 py-2 bg-white/10 rounded-lg text-black placeholder-white/60 focus:outline-none focus:ring-2 focus:ring-white/20"
                            />
                            <button
                                className="absolute right-3 top-2.5 cursor-pointer hover:text-white/80 transition-colors"
                            >
                            </button>
                        </div>
                    </div>
                    <nav className="flex items-center space-x-1">
                        <NavLink to="/books" className={linkStyle}>
                            Books
                        </NavLink>
                        <NavLink to="/borrowers" className={linkStyle}>
                            Borrowers
                        </NavLink>
                        <NavLink to="/reservations" className={linkStyle}>
                            Reservations
                        </NavLink>
                    </nav>
                </div>
            </div>
        </header>
    );
}
