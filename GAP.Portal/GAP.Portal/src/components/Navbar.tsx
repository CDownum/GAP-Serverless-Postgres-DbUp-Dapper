import { Link } from '@tanstack/react-router';
import { Guid } from 'guid-typescript';
import { useFetchUser } from '../hooks/userData';

const NavBar: React.FC = () => {
  const { data, error, isLoading } = useFetchUser(1, Guid.parse('d10f9784-31bd-45aa-b9a3-b4e92c4e118b'));

  if(isLoading) return <div>Loading...</div>
  if(error) return <div>Error:</div>
  
  return (
    <>
      <nav className="navbar navbar-expand-lg navbar-dark bg-dark" aria-label="Fifth navbar example">
        <div className="container-fluid">
          
          <a className="navbar-brand" href="#"> <svg xmlns="http://www.w3.org/2000/svg" width="35" height="35" fill="currentColor" className="bi bi-rocket-takeoff" viewBox="0 0 16 16">
            <path d="M9.752 6.193c.599.6 1.73.437 2.528-.362s.96-1.932.362-2.531c-.599-.6-1.73-.438-2.528.361-.798.8-.96 1.933-.362 2.532"></path>
            <path d="M15.811 3.312c-.363 1.534-1.334 3.626-3.64 6.218l-.24 2.408a2.56 2.56 0 0 1-.732 1.526L8.817 15.85a.51.51 0 0 1-.867-.434l.27-1.899c.04-.28-.013-.593-.131-.956a9 9 0 0 0-.249-.657l-.082-.202c-.815-.197-1.578-.662-2.191-1.277-.614-.615-1.079-1.379-1.275-2.195l-.203-.083a10 10 0 0 0-.655-.248c-.363-.119-.675-.172-.955-.132l-1.896.27A.51.51 0 0 1 .15 7.17l2.382-2.386c.41-.41.947-.67 1.524-.734h.006l2.4-.238C9.005 1.55 11.087.582 12.623.208c.89-.217 1.59-.232 2.08-.188.244.023.435.06.57.093q.1.026.16.045c.184.06.279.13.351.295l.029.073a3.5 3.5 0 0 1 .157.721c.055.485.051 1.178-.159 2.065m-4.828 7.475.04-.04-.107 1.081a1.54 1.54 0 0 1-.44.913l-1.298 1.3.054-.38c.072-.506-.034-.993-.172-1.418a9 9 0 0 0-.164-.45c.738-.065 1.462-.38 2.087-1.006M5.205 5c-.625.626-.94 1.351-1.004 2.09a9 9 0 0 0-.45-.164c-.424-.138-.91-.244-1.416-.172l-.38.054 1.3-1.3c.245-.246.566-.401.91-.44l1.08-.107zm9.406-3.961c-.38-.034-.967-.027-1.746.163-1.558.38-3.917 1.496-6.937 4.521-.62.62-.799 1.34-.687 2.051.107.676.483 1.362 1.048 1.928.564.565 1.25.941 1.924 1.049.71.112 1.429-.067 2.048-.688 3.079-3.083 4.192-5.444 4.556-6.987.183-.771.18-1.345.138-1.713a3 3 0 0 0-.045-.283 3 3 0 0 0-.3-.041Z"></path>
            <path d="M7.009 12.139a7.6 7.6 0 0 1-1.804-1.352A7.6 7.6 0 0 1 3.794 8.86c-1.102.992-1.965 5.054-1.839 5.18.125.126 3.936-.896 5.054-1.902Z"></path>
          </svg>  New Wings</a>
          <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarsExample05" aria-controls="navbarsExample05" aria-expanded="false" aria-label="Toggle navigation">
            <span className="navbar-toggler-icon"></span>
          </button>

        <div className="collapse navbar-collapse" id="navbarsExample05">
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">      
          <li className="nav-item">
          <Link to="/" className="nav-link text-white">
            <i className="bi bi-house-fill"/> Home
          </Link>
          </li>
          <li>
          <Link to="/about" className="nav-link text-white">
            <i className="bi bi-person"/> Users
          </Link>
          </li> 
          <li className="nav-item dropdown">
            <a className="nav-link dropdown-toggle" href="#" id="dropdown05" data-bs-toggle="dropdown" aria-expanded="false">GAP</a>
            <ul className="dropdown-menu" aria-labelledby="dropdown05">               
              <li><a className="dropdown-item" href="/salesGoals">1- Financial WorkSheet</a></li>
              <li><a className="dropdown-item" href="#">2 - Sales Goals</a></li>
              <li><a className="dropdown-item" href="#">3 - Linking Goals & Sales</a></li>
              <li><a className="dropdown-item" href="#">4 - Matching to Sources</a></li>
              <li><a className="dropdown-item" href="#">5 - Key Goals Worksheet</a></li>
              <li><a className="dropdown-item" href="#">6 - Internet</a></li>
              <li><a className="dropdown-item" href="#">7 - Realtors</a></li>
              <li><a className="dropdown-item" href="#">8 - Referrels</a></li>
              <li><a className="dropdown-item" href="#">9 - Self-Originated</a></li>
              <li><a className="dropdown-item" href="#">10 - Walk-In</a></li>
              <li><a className="dropdown-item" href="#">11 - Follow-Up</a></li>
            </ul>
          </li>
          </ul>
          <form>
            <input className="form-control" type="text" placeholder="Search" aria-label="Search" style={{}} />
          </form>
            <div className="dropdown m-4">
            <a href="#" className="d-flex align-items-center text-white text-decoration-none dropdown-toggle" id="dropdownUser1" data-bs-toggle="dropdown" aria-expanded="false">
              <img src="https://github.com/mdo.png" alt="" width="32" height="32" className="rounded-circle me-2" />
              <strong>{data ? data.DisplayName : 'Guest'}</strong>
            </a>
            <ul className="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="dropdownUser1">      
              <li><a className="dropdown-item" href="#">Settings</a></li>
              <li><a className="dropdown-item" href="#">Profile</a></li>
              <li><hr className="dropdown-divider"/></li>
              <li><a className="dropdown-item" href="#">Sign out</a></li>
            </ul>
            </div>
        </div>
      </div>
      </nav>
    </>
  );
};

export default NavBar;