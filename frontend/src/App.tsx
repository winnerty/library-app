import { BrowserRouter } from "react-router-dom"
import Layout from "./components/Layout"
import AppRoutes from "./routes/AppRoutes"
import React from "react"
import { SearchProvider } from "./context/SearchContext"

export default function App() {
    return (
        <BrowserRouter>
            <SearchProvider>
                <Layout>
                    <AppRoutes />
                </Layout>
            </SearchProvider>
        </BrowserRouter>
    )
}