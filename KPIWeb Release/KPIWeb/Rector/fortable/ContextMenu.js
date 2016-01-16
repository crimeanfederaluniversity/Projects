
function cont() {
document.oncontextmenu = function() {return false;};
    // Вешаем слушатель события нажатие кнопок мыши для всего документа:

    $('.m').live('mousedown',function(event) {
        
        // Убираем css класс selected-html-element у абсолютно всех элементов на странице с помощью селектора "*":
        //$('*').removeClass('selected-html-element');
        // Удаляем предыдущие вызванное контекстное меню:
        $('.context-menu').remove();
        
        // Проверяем нажата ли именно правая кнопка мыши:
        if (event.which === 3)  {
            
            // Получаем элемент на котором был совершен клик:
            var target = $(event.target);
            
            // Добавляем класс selected-html-element что бы наглядно показать на чем именно мы кликнули (исключительно для тестирования):
            target.addClass('selected-html-element');

            // Создаем меню:
            $('<div/>', {
                class: 'context-menu' // Присваиваем блоку наш css класс контекстного меню:
            })
            .css({
                left: event.pageX+'px', // Задаем позицию меню на X
                top: event.pageY+'px' // Задаем позицию меню по Y
            })
            .appendTo('body') // Присоединяем наше меню к body документа:
            .append( // Добавляем пункты меню:
                 $('<ul/>').append('<li><a href="javascript:rem();">Remove element</a></li>')
                                .append('<li><a href="javascript:add();">Copy element</a></li>')
                                .append('<li><a href="#">Element style</a></li>') 
                                .append('<li><a href="#">Element props</a></li>')
                                .append('<li><a href="#">Open Inspector</a></li>')
                   )
             .show('slow'); // Показываем меню с небольшим стандартным эффектом jQuery. Как раз очень хорошо подходит для меню
         }
    });
};
