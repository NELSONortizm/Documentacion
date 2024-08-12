sp_helpdb BizTalkMsgBoxDb

backup log BizTalkMsgBoxDb to disk = N'd:\backupLogBizTalkMsgBoxDb'

dbcc shrinkfile ('BizTalkMsgBoxDb_log',0,truncateonly)

--Antes de truncar el log cambiamos el modelo de recuperación a SIMPLE.
ALTER DATABASE BizTalkMsgBoxDb
SET RECOVERY SIMPLE;
GO
--Reducimos el log de transacciones a  1 MB.
DBCC SHRINKFILE(BizTalkMsgBoxDb_log, 1);
GO
-- Cambiamos nuevamente el modelo de recuperación a Completo.
ALTER DATABASE BizTalkMsgBoxDb
SET RECOVERY FULL;
GO