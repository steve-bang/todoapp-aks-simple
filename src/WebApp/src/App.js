import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import LandingPage from './components/LandingPage';
import TodoPage from './components/Todo';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LandingPage />} />
        <Route path="/todos" element={<TodoPage />} />
      </Routes>
    </Router>
  );
}

export default App;