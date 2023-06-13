import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import styled from 'styled-components';

import './App.css'
import StockholmWeather from './components/Home';
import Statistices from './components/Statistices';
import SaveCities from './components/SaveCities';
import GetCities from './components/GetCities';



function App() {

  return (
    <>
    <StockholmWeather>
    </StockholmWeather>
    <Statistices>
    </Statistices>
    <SaveCities>
    </SaveCities>
    <GetCities>
    </GetCities>
    
    </>
  )
}

export default App
