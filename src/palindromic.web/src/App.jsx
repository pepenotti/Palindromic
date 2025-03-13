import { useState } from 'react'
import './App.css'

function App() {
  const [input1, setInput1] = useState('')
  const [input2, setInput2] = useState('')
  const [result, setResult] = useState(null)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState(null)

  const handleSubmit = async (e) => {
    e.preventDefault()
    setLoading(true)
    setError(null)
    try {
      const response = await fetch(env.AZURE_FUNCTION_URL, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ input1, input2 }),
      })
      if (!response.ok) {
        throw new Error('Network response was not ok')
      }
      const data = await response.json()
      setResult(data)
    } catch (error) {
      setError('Failed to fetch data. Please try again.')
      console.error('Error:', error)
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="container">
      <h1>Find the highest palindrome between</h1>
      <form onSubmit={handleSubmit} className="form">
        <input
          type="number"
          value={input1}
          onChange={(e) => setInput1(e.target.value)}
          placeholder="From"
          className="input"
          min="-2147483648"
          max="2147483647"
        />
        <input
          type="number"
          value={input2}
          onChange={(e) => setInput2(e.target.value)}
          placeholder="To"
          className="input"
          min="-2147483648"
          max="2147483647"
        />
        <br />
        <button type="submit" className="button" disabled={loading}>
          {loading ? 'Loading...' : 'Submit'}
        </button>
      </form>
      {loading && <p className="loader">Loading...</p>}
      {error && <p className="error">{error}</p>}
      {result && <p className="result">Result: {JSON.stringify(result)}</p>}
    </div>
  )
}

export default App