const express = require('express');
const http = require('http');
const socketIO = require('socket.io');
const robot = require('robotjs');

const app = express();
const server = http.createServer(app);
const io = socketIO(server);

const PORT = 3000;

app.get('/', (req, res) => {
  res.sendFile(__dirname + '/index.html');
});

io.on('connection', (socket) => {
    console.log('Bir kullanıcı bağlandı:', socket.id);
  
    // Klavye tuşları dinleme
    socket.on('keydown', (keyCode) => {
      console.log('Tuş basıldı:', keyCode);
      // İstediğiniz işlemleri gerçekleştirin
    });
  
    socket.on('keyup', (keyCode) => {
      console.log('Tuş bırakıldı:', keyCode);
      // İstediğiniz işlemleri gerçekleştirin
    });
  
    // Mouse hareketleri dinleme
    socket.on('mousemove', (data) => {
      console.log('Mouse hareketi:', data);
      // İstediğiniz işlemleri gerçekleştirin
    });
  });
  
server.listen(PORT, () => {
    console.log(`Server http://localhost:${PORT} adresinde çalışıyor`);
});