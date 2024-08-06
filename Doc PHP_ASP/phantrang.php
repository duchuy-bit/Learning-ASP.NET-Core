<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Frameset//EN">
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <title>Thông tin khách hàng</title>
    <style>
        table {
            margin-left: auto;
            margin-right: auto;
            border-collapse: collapse;
            width: 50%;
        }

        th, td {
            border: solid 0.5px;
            text-align: left;
            padding: 8px;
        }
        tr:nth-child(even) {background-color: #f2f2f2;}
        .center {
            margin-left: auto;
            margin-right: auto;
            text-align: center;
        }
    </style>
</head>
<body>
<?php
    require('connect.php');
    $conn = mysqli_connect($servername, $username, $password, $dbname) or die ('Không thể kết nối tới database'. mysqli_connect_error());
    mysqli_set_charset($conn, 'utf8');
    //phan trang
    $rowsPerPage=5;
    if ( ! isset($_GET['page']))
        $_GET['page'] = 1;
    $offset =($_GET['page']-1)*$rowsPerPage;
    //$query="Select * from sua LIMIT $offset, $rowsPerPage";
    $query="SELECT `sua`.`Ten_sua`, `sua`.`Trong_luong`, `sua`.`Don_gia`, `hang_sua`.`Ten_hang_sua`, `loai_sua`.`Ten_loai` 
    FROM sua CROSS JOIN hang_sua CROSS JOIN loai_sua WHERE `sua`.`Ma_hang_sua`=`hang_sua`.`Ma_hang_sua` and `sua`.`Ma_loai_sua`= `loai_sua`.`Ma_loai_sua` LIMIT $offset, $rowsPerPage;";
    $result = mysqli_query($conn,$query);
    if (!$result ) die ('<br> <b>Query failed</b>');
    $numRows= mysqli_num_rows($result);
    $maxPage = ceil($numRows / $rowsPerPage);
    if($numRows<>0)
    {
        echo "<div style='overflow-x: auto;'>
            <table>
                <tr style='background:pink;'><th colspan='6'><p class='center'>THÔNG TIN SỮA</p></th></tr>
                <tr>
                    <th class='center'>STT</th>
                    <th>Tên Sữa</th>
                    <th>Hãng Sữa</th>
                    <th>Loại Sữa</th>
                    <th>Trọng Lượng</th>
                    <th>Đơn giá</th>	
                </tr>";
        $temp=$_GET['page']*$rowsPerPage;// danh so thu tu
        if($temp<=$rowsPerPage) $num=0;
        else $num=$temp-$rowsPerPage;
        while($rows=mysqli_fetch_array($result))
        {
            $num++;
            echo "<tr>";
                echo "<td>".$num."</td>";
                echo "<td>{$rows['Ten_sua']}</td>";
                echo "<td>{$rows['Ten_hang_sua']}</td>";
                echo "<td>{$rows['Ten_loai']}</td>";
                echo "<td>{$rows['Trong_luong']} gram</td>";
                echo "<td>{$rows['Don_gia'] } VNĐ</td>";
            echo "</tr>";
        }
        echo"</table> </div>";
        $re = mysqli_query($conn, 'select * from sua');
        $numRows = mysqli_num_rows($re);
        $maxPage = floor($numRows/$rowsPerPage) + 1;
        echo "<div class='center'>";
        if ($_GET['page'] > 1){
            echo "<a  href=" .$_SERVER['PHP_SELF']."?page=".($_GET['page']-1).">Back</a> "; //gắn thêm nút Back
        }
        for ($i=1 ; $i<=$maxPage ; $i++)
        {
            if ($i == $_GET['page'])
            {
                echo '<b class="center">Trang'.$i.'</b> '; //trang hiện tại sẽ được bôi đậm
            }
            else {
                echo "<a  href=" . $_SERVER['PHP_SELF'] . "?page=" . $i . ">Trang " . $i . "</a> ";
            }
        }
        if ($_GET['page'] < $maxPage) {
            echo "<a href=" . $_SERVER['PHP_SELF'] . "?page=" . ($_GET['page'] + 1) . ">Next</a>";  //gắn thêm nút Next
        }
        echo"</div>";
    //    echo 'Tong so trang la: '.$maxPage;
    }
    mysqli_close($conn);
?>
</body>
</html>