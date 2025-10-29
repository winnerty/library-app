
import { BrowserRouter } from "react-router-dom"
import Sidebar from "./components/Sidebar"
import AppRoutes from "./routes/AppRoutes"
import React from "react"

export default function App() {
    return (
        <BrowserRouter>
            <div className="flex">
                <Sidebar />
                <div className="flex-1 bg-white">
                    <AppRoutes />
                </div>
            </div>
        </BrowserRouter>
    )
}