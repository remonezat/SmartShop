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

  $('#bills').click(function(){
    $('.bills').fadeToggle()
    $('#bills .up').fadeToggle(500)
    $('#bills .down').fadeToggle(400)

  })

  $('#expensess').click(function(){
    $('.expensess').fadeToggle()
    $('#expensess .up').fadeToggle(500)
    $('#expensess .down').fadeToggle(400)

  })

  $('#employee').click(function(){
    $('.employee').fadeToggle()
    $('#employee .up').fadeToggle(500)
    $('#employee .down').fadeToggle(400)

  })


  $('#accounts').click(function(){
    $('.accounts').fadeToggle()
    $('#accounts .up').fadeToggle(500)
    $('#accounts .down').fadeToggle(400)

  })


  $('#items').click(function(){
    $('.items').fadeToggle()
    $('#items .up').fadeToggle(500)
    $('#items .down').fadeToggle(400)

  })

  $('#transaction').click(function(){
    $('.transaction').fadeToggle()
    $('#transaction .up').fadeToggle(500)
    $('#transaction .down').fadeToggle(400)

  })


  $('#reports').click(function(){
    $('.reports').fadeToggle()
    $('#reports .up').fadeToggle(500)
    $('#reports .down').fadeToggle(400)

  })


  $('.popup .add').click(function(){
    $('.sale').fadeToggle()
   
  })

  $('.showPopup').click(function(){
    $('.popup').fadeIn()
    $('.cover').fadeIn()

  })
