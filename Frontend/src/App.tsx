
import { useState } from 'react'
import './App.css'

type Registration = {
  id: number
  registrationNumber: string
  participantName: string
  carMakeModel: string
  registrationType: string
  status: string
  paymentStatus: string
}

const sampleRegistrations: Registration[] = [
  { id: 1, registrationNumber: 'CSR-2026-001', participantName: 'Juan Dela Cruz', carMakeModel: 'Toyota Vios', registrationType: 'Car Show', status: 'Confirmed', paymentStatus: 'Paid' },
  { id: 2, registrationNumber: 'CSR-2026-002', participantName: 'Maria Santos', carMakeModel: 'Honda Civic', registrationType: 'Car Show', status: 'Pending', paymentStatus: 'Unpaid' },
  { id: 3, registrationNumber: 'CSR-2026-003', participantName: 'Pedro Reyes', carMakeModel: 'Mitsubishi Lancer', registrationType: 'Car Meet', status: 'Checked In', paymentStatus: 'Paid' },
  { id: 4, registrationNumber: 'CSR-2026-004', participantName: 'Ana Garcia', carMakeModel: 'Nissan GT-R', registrationType: 'Car Show', status: 'Confirmed', paymentStatus: 'Paid' },
  { id: 5, registrationNumber: 'CSR-2026-005', participantName: 'Mark Lopez', carMakeModel: 'Subaru WRX', registrationType: 'Car Meet', status: 'Cancelled', paymentStatus: 'Unpaid' },
  { id: 6, registrationNumber: 'CSR-2026-006', participantName: 'Sofia Ramos', carMakeModel: 'Mazda 3', registrationType: 'Car Show', status: 'Pending', paymentStatus: 'Partial' },
]

function App() {
  const [search, setSearch] = useState('')

  const filteredRegistrations = sampleRegistrations.filter((registration) =>
    registration.participantName.toLowerCase().includes(search.toLowerCase()) ||
    registration.registrationNumber.toLowerCase().includes(search.toLowerCase())
  )

  return (
    <main className="dashboard">
      <header className="dashboard-header">
        <div>
          <p className="eyebrow">CAR SHOW MANAGEMENT</p>
          <h1>Registration Dashboard</h1>
          <p className="subtitle">Manage and monitor car show registrations.</p>
        </div>
        <button className="add-button">+ New Registration</button>
      </header>

      <section className="stats-grid">
        <article className="stat-card">
          <p>Total Registrations</p>
          <h2>32</h2>
        </article>
        <article className="stat-card">
          <p>Pending</p>
          <h2>14</h2>
        </article>
        <article className="stat-card">
          <p>Confirmed</p>
          <h2>6</h2>
        </article>
        <article className="stat-card">
          <p>Checked In</p>
          <h2>6</h2>
        </article>
        <article className="stat-card">
          <p>Cancelled</p>
          <h2>6</h2>
        </article>
      </section>

      <section className="registrations-section">
        <div className="section-header">
          <div>
            <h2>Recent Registrations</h2>
            <p>View and manage participant registrations.</p>
          </div>
          <input
            type="text"
            placeholder="Search participant or registration..."
            value={search}
            onChange={(event) => setSearch(event.target.value)}
          />
        </div>

        <div className="table-container">
          <table>
            <thead>
              <tr>
                <th>Registration No.</th>
                <th>Participant</th>
                <th>Car Model</th>
                <th>Type</th>
                <th>Status</th>
                <th>Payment</th>
              </tr>
            </thead>
            <tbody>
              {filteredRegistrations.map((registration) => (
                <tr key={registration.id}>
                  <td>{registration.registrationNumber}</td>
                  <td>{registration.participantName}</td>
                  <td>{registration.carMakeModel}</td>
                  <td>{registration.registrationType}</td>
                  <td>
                    <span className={`status ${registration.status.toLowerCase().replace(' ', '-')}`}>
                      {registration.status}
                    </span>
                  </td>
                  <td>{registration.paymentStatus}</td>
                </tr>
              ))}
              {filteredRegistrations.length === 0 && (
                <tr>
                  <td colSpan={6} className="empty-message">
                    No registrations found.
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </section>
    </main>
  )
}

export default App
