/* eslint-disable no-unused-vars */
/* eslint-disable no-debugger */
/* eslint-disable react/prop-types */
import { useState, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { LoginContext } from '../../state/context';
import { login } from './api';
import './Login.css';
import signInImage from '../../assets/deneme2.png';

export function Login() {

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const { onLoginSuccess } = useContext(LoginContext);
    const navigate = useNavigate();
  
    const onSubmit = async (event) => {
      event.preventDefault();
      try {
        const data = await login({ email, password });
        onLoginSuccess(data);
        navigate('/');
      } catch (err) {
        setError('Invalid email or password');
      }
    };
  
    return (
      <div className="login-container">
         <div className="right-div">
        <img src={signInImage} alt="Sign In Illustration" />
      </div>

      <div className="left-div">
      <div className="form-section">
          <div className="card-header bg-primary text-white">
            <h1>Login</h1>
          </div>
          <div className="card-body">
            <form onSubmit={onSubmit}>
              <div className="mb-3">
                <label htmlFor="email" className="form-label">Email</label>
                <input
                  id="email"
                  type="email"
                  className="form-control"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                />

              </div>
              <div className="mb-3">
                <label htmlFor="password" className="form-label">Password</label>
                <input
                  id="password"
                  type="password"
                  className="form-control"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                />
              </div>
              {error && <div className="alert alert-danger">{error}</div>}
              <div className="text-center">
                <button className="btn btn-primary">Giriş Yap</button>
              </div>
              <div className="text-center">
                <button className="btn-link" onClick={() => navigate('/signup')}>Üye Değil Misiniz? Hemen Üye Olun!</button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
    );
  }
