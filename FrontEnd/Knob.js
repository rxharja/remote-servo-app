import React from 'react';
import { Knob } from "react-rotary-knob";
import axios from 'axios';

export default class App extends React.Component {
 
  state = {
    value: 0
  }

  changeValue(val) {
    const maxDistance = 90;
    let distance = Math.abs(val - this.state.value);
    if (distance > maxDistance) {
      return
    } else {
      if (Math.floor(val) !== Math.floor(this.state.value)) {
        this.setState({value:Math.floor(val)})
          axios.post("http://192.168.1.21:5000/arduino/setangle?angle=" + this.state.value)
            .then( response => { console.log(response)})
            .catch( error => error );
      }
    }
  }
 
  render() {
    return <Knob 
      onChange={this.changeValue.bind(this)} 
      min={0} 
      max={181} 
      unlockDistance={0}
      preciseMode={false}
      width={1000}
      height={1000}
      value={this.state.value}
      />
  }
}
