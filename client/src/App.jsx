import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import styled from 'styled-components';

import './App.css'
import StockholmWeather from './components/Home';
import Statistices from './components/Statistices';

const MovieTitle = styled.h1`
  margin: 3em 2em ;
  color: #481fcd;
  font-size: 24px;
`;
function App() {

  return (
    <>
    <StockholmWeather>
    </StockholmWeather>
    <Statistices>
    </Statistices>
    </>
  )
}

export default App
