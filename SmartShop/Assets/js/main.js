var loadFile = function(event) {
    $('.imgoutput').css('background','#FFF')
    var image = document.getElementById('output');
    image.src = URL.createObjectURL(event.target.files[0]);
  };
  


  $('.showTable').click(function(){
    $('.hiddenTable').fadeToggle()
    $('.showTable .up').fadeToggle(500)
    $('.showTable .down').fadeToggle(400)

  })


  $('.popup .add').click(function(){
    $('.sale').fadeToggle()
   
  })

  $('.showPopup').click(function(){
    $('.popup').fadeIn()
    $('.cover').fadeIn()

  })
